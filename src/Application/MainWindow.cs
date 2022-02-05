using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using winusbdotnet.UsbDevices;

namespace SeekOFix
{
    public partial class MainWindow : Form
    {
        SeekThermal thermal;
        Thread thermalThread;
        ThermalFrame currentFrame, lastUsableFrame;

        bool stopThread;
        bool grabExternalReference = false;
        bool firstAfterCal = false;
        bool usingExternalCal = false;
        bool autoSaveImg = false;
        bool autoRange = true;
        bool dynSliders = false;
        bool sharpenImage = false;
        bool isRunning = true;

        string tempUnit = "K";

        ushort[] arrID4 = new ushort[Constants.DATA_LENGTH];
        ushort[] arrID1 = new ushort[Constants.DATA_LENGTH];
        ushort[] arrID3 = new ushort[Constants.DATA_LENGTH];

        bool[] badPixelArr = new bool[Constants.DATA_LENGTH];

        ushort[] gMode = new ushort[1000];

        ushort[,] paletteArr = new ushort[1001, 3]; //-> ushort

        byte[] imgBuffer = new byte[Constants.DATA_LENGTH * 3];

        ushort gModePeakIx = 0;
        ushort gModePeakCnt = 0;
        ushort gModeLeft = 0;
        ushort gModeRight = 0;
        ushort gModeLeftManual = 0;
        ushort gModeRightManual = 0;
        ushort avgID4 = 0;
        ushort avgID1 = 0;
        ushort maxTempRaw = 0;

        double[] gainCalArr = new double[Constants.DATA_LENGTH];

        Bitmap bitmap = new Bitmap(Constants.DATA_W, Constants.DATA_H, PixelFormat.Format24bppRgb);
        Bitmap croppedBitmap = new Bitmap(Constants.IMAGE_W, Constants.IMAGE_H, PixelFormat.Format24bppRgb);
        Bitmap bigBitmap = new Bitmap(Constants.IMAGE_W * 2, Constants.IMAGE_H * 2, PixelFormat.Format24bppRgb);
        BitmapData bitmapData;

        Crop cropFilter = new Crop(new Rectangle(0, 0, Constants.IMAGE_W, Constants.IMAGE_H));
        ResizeBilinear resizeFilter = new ResizeBilinear(Constants.IMAGE_W * 2, Constants.IMAGE_H * 2);
        Sharpen sharpenFilter = new Sharpen();

        Output.VideoRecorder recorder = null;

        public MainWindow()
        {
            InitializeComponent();

            var localPath = Directory.GetCurrentDirectory().ToString();
            var exportPath = localPath + @"\Export";
            Directory.CreateDirectory(exportPath);
            outputPathField.Text = exportPath;

            var device = SeekThermal.Enumerate().FirstOrDefault();

            if (device == null)
            {
                MessageBox.Show("No Seek Thermal devices found.");
                return;
            }

            thermal = new SeekThermal(device);

            thermalThread = new Thread(ThermalThreadProc);
            thermalThread.IsBackground = true;
            thermalThread.Start();
        }

        void ToggleThreadActivity()
        {
            if (isRunning)
                thermalThread.Suspend();
            else
                thermalThread.Resume();

            isRunning = !isRunning;
        }

        void ThermalThreadProc()
        {
            while (!stopThread && thermal != null)
            {
                bool progress = false;

                currentFrame = thermal.GetFrameBlocking();

                switch (currentFrame.StatusByte)
                {
                    // Gain calibration
                    case 4:
                        Frame4Stuff();

                        break;

                    // Shutter calibration
                    case 1:
                        MarkBadPixels();

                        if (!usingExternalCal)
                            Frame1Stuff();

                        firstAfterCal = true;

                        break;

                    // Image frame
                    case 3:
                        MarkBadPixels();

                        // Use this image as reference
                        if (grabExternalReference)
                        {
                            grabExternalReference = false;
                            usingExternalCal = true;
                            Frame1Stuff();
                        }
                        else
                        {
                            Frame3Stuff();
                            lastUsableFrame = currentFrame;
                            progress = true;
                        }

                        break;

                    default:
                        break;
                }

                if (progress)
                {
                    // Redraw form
                    Invalidate();
                }
            }
        }

        private void Frame4Stuff()
        {
            arrID4 = currentFrame.RawDataU16;
            avgID4 = GetMode(arrID4);

            for (int i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if (arrID4[i] > 2000 && arrID4[i] < 8000)
                {
                    gainCalArr[i] = avgID4 / (double) arrID4[i];
                }
                else
                {
                    gainCalArr[i] = 1;
                    badPixelArr[i] = true;
                }
            }
        }

        private void Frame1Stuff()
        {
            arrID1 = currentFrame.RawDataU16;
            //avgID1 = GetMode(arrID1);
        }

        private void Frame3Stuff()
        {
            arrID3 = currentFrame.RawDataU16;

            for (int i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if (arrID3[i] > 2000)
                {
                    arrID3[i] = (ushort) ((arrID3[i] - arrID1[i]) * gainCalArr[i] + 7500);
                }
                else
                {
                    arrID3[i] = 0;
                    badPixelArr[i] = true;
                }
            }

            FixBadPixels();
            RemoveNoise();
            GetHistogram();
            FillImgBuffer();
        }

        private void FillImgBuffer()
        {
            double iScaler = (double) (gModeRight - gModeLeft) / 1000;

            for (int i = 0; i < Constants.DATA_LENGTH; i++)
            {
                ushort v = arrID3[i];
                if (v < gModeLeft) v = gModeLeft;
                if (v > gModeRight) v = gModeRight;
                v = (ushort) (v - gModeLeft);
                ushort loc = (ushort) (v / iScaler);

                imgBuffer[i * 3] = (byte) paletteArr[loc, 2];
                imgBuffer[i * 3 + 1] = (byte) paletteArr[loc, 1];
                imgBuffer[i * 3 + 2] = (byte) paletteArr[loc, 0];
            }
        }

        private void MarkBadPixels()
        {
            ushort[] rawDataArr = currentFrame.RawDataU16;

            for (int i = 0; i < rawDataArr.Length; i++)
            {
                if (rawDataArr[i] < 2000 || rawDataArr[i] > 22000)
                {
                    badPixelArr[i] = true;
                }
            }
        }

        private void FixBadPixels()
        {
            ushort i = 0;

            for (ushort y = 0; y < Constants.DATA_H; y++)
            {
                for (ushort x = 0; x < Constants.DATA_W; x++)
                {
                    if (badPixelArr[i] && x < Constants.IMAGE_W)
                    {
                        ushort val = 0;
                        ushort nr = 0;

                        // Top pixel
                        if (y > 0 && !badPixelArr[i - Constants.DATA_W])
                        {
                            val += arrID3[i - Constants.DATA_W];
                            ++nr;
                        }

                        // Bottom pixel
                        if (y < (Constants.IMAGE_H - 1) && !badPixelArr[i + Constants.DATA_W])
                        {
                            val += arrID3[i + Constants.DATA_W];
                            ++nr;
                        }

                        // Left pixel
                        if (x > 0 && !badPixelArr[i - 1])
                        {
                            val += arrID3[i - 1];
                            ++nr;
                        }

                        // Right pixel
                        if (x < (Constants.IMAGE_W - 1) && !badPixelArr[i + 1])
                        {
                            val += arrID3[i + 1];
                            ++nr;
                        }

                        if (nr > 0)
                        {
                            val /= nr;
                            arrID3[i] = val;
                        }
                    }

                    i++;
                }
            }
        }

        private void RemoveNoise()
        {
            ushort i = 0;
            ushort[] arrColor = new ushort[4];

            for (ushort y = 0; y < Constants.DATA_H; y++)
            {
                for (ushort x = 0; x < Constants.DATA_W; x++)
                {
                    if (x > 0 && x < Constants.IMAGE_W && y > 0 && y < (Constants.IMAGE_H - 1))
                    {
                        arrColor[0] = arrID3[i - Constants.DATA_W]; // Top
                        arrColor[1] = arrID3[i + Constants.DATA_W]; // Bottom
                        arrColor[2] = arrID3[i - 1]; // Left
                        arrColor[3] = arrID3[i + 1]; // Right

                        ushort val = (ushort) ((arrColor[0] + arrColor[1] + arrColor[2] + arrColor[3] - Highest(arrColor) - Lowest(arrColor)) / 2);

                        if (Math.Abs(val - arrID3[i]) > 100 && val != 0)
                        {
                            arrID3[i] = val;
                        }
                    }

                    i++;
                }
            }
        }

        private ushort Highest(ushort[] numbers)
        {
            ushort highest = 0;

            for (ushort i = 0; i < 4; i++)
            {
                if (numbers[i] > highest)
                    highest = numbers[i];
            }

            return highest;
        }

        private ushort Lowest(ushort[] numbers)
        {
            ushort lowest = 30000;

            for (ushort i = 0; i < 4; i++)
            {
                if (numbers[i] < lowest)
                    lowest = numbers[i];
            }

            return lowest;
        }

        public ushort GetMode(ushort[] arr)
        {
            ushort[] arrMode = new ushort[320];

            for (ushort i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if ((arr[i] > 1000) && (arr[i] / 100 != 0)) arrMode[arr[i] / 100]++;
            }

            ushort topPos = (ushort) Array.IndexOf(arrMode, arrMode.Max());

            return (ushort) (topPos * 100);
        }

        public void GetHistogram()
        {
            maxTempRaw = arrID3.Max();

            ushort[] arrMode = new ushort[2100];

            for (ushort i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if ((arrID3[i] > 1000) && (arrID3[i] / 10 != 0) && !badPixelArr[i]) arrMode[arrID3[i] / 10]++;
            }

            ushort topPos = (ushort) Array.IndexOf(arrMode, arrMode.Max());

            gMode = arrMode;
            gModePeakCnt = arrMode.Max();
            gModePeakIx = (ushort) (topPos * 10);

            // Lower it to 100px
            for (ushort i = 0; i < 2100; i++)
            {
                gMode[i] = (ushort) ((double) arrMode[i] / gModePeakCnt * 100);
            }

            if (autoRange)
            {
                gModeLeft = gModePeakIx;
                gModeRight = gModePeakIx;

                // Find left border
                for (ushort i = 0; i < topPos; i++)
                {
                    if (arrMode[i] > arrMode[topPos] * 0.01)
                    {
                        gModeLeft = (ushort) (i * 10);
                        break;
                    }
                }

                // Find right border
                for (ushort i = 2099; i > topPos; i--)
                {
                    if (arrMode[i] > arrMode[topPos] * 0.01)
                    {
                        gModeRight = (ushort) (i * 10);
                        break;
                    }
                }

                gModeLeftManual = gModeLeft;
                gModeRightManual = gModeRight;
            }
            else
            {
                gModeLeft = gModeLeftManual;
                gModeRight = gModeRightManual;
            }
        }

        public void DrawHistogram()
        {
            int imgWidth = (gModeRight - gModeLeft) / 10;
            int leftBorder = gModeLeft / 10;
            var hist = new Bitmap(imgWidth, 100, PixelFormat.Format24bppRgb);
            Pen blackPen = new Pen(Color.Black, 1);

            using (var g = Graphics.FromImage(hist))
            {
                g.FillRectangle(new SolidBrush(Color.White), 0, 0, imgWidth, 100);

                for (int i = 0; i < imgWidth; i++)
                {
                    g.DrawLine(blackPen, i, 0, i, gMode[leftBorder + i]);
                }
            }

            hist.RotateFlip(RotateFlipType.Rotate180FlipX);
            hist = new Bitmap(hist, new Size(200, 100));

            histogramPicture.Image = hist;
        }

        private void UpdateAnalyzablePictureBox(AnalyzablePictureBox box, Bitmap image, ushort[] rawValues)
        {
            box.Image = image;
            box.RawValues = rawValues;
            box.Reanalyze();
        }

        private void UpdateAnalyzablePictureBoxSize(AnalyzablePictureBox box)
        {
            if (box.Parent == null || box.Image == null) return;

            var pw = (double) box.Parent.Width;
            var ph = (double) box.Parent.Height;
            var iw = (double) box.Image.Width;
            var ih = (double) box.Image.Height;

            var scale = Math.Min(pw / iw, ph / ih);

            var w = iw * scale;
            var h = ih * scale;

            box.Location = new Point((int) ((pw - w) / 2.0),
                                     (int) ((ph - h) / 2.0));
            box.Size = new Size((int) w, (int) h);
        }

        private void HandleLoad(object sender, EventArgs e)
        {
            var pngFiles = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Palette").GetFiles("*.png");

            foreach (FileInfo file in pngFiles)
            {
                paletteCombo.Items.Add(new ComboItem(file.FullName, file.Name.Replace(".png", "")));
            }

            paletteCombo.SelectedIndex = 1;
        }

        private void HandlePaint(object sender, PaintEventArgs e)
        {
            if (lastUsableFrame != null)
            {
                string minTemp = Utils.FormatTempString(tempUnit, gModeLeft);
                string maxTemp = Utils.FormatTempString(tempUnit, gModeRight);

                tempGaugeMinLabel.Text = minTemp;
                tempGaugeMaxLabel.Text = maxTemp;
                sliderMinTempLabel.Text = minTemp;
                sliderMaxTempLabel.Text = maxTemp;

                gModeLeftLabel.Text = gModeLeft.ToString();
                gModeRightLabel.Text = gModeRight.ToString();
                maxTempRawLabel.Text = maxTempRaw.ToString();

                // Set sliders position
                if (autoRange)
                {
                    minTempSlider.Value = gModeLeft;
                    maxTempSlider.Value = gModeRight;
                }

                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                Marshal.Copy(imgBuffer, 0, bitmapData.Scan0, imgBuffer.Length);
                bitmap.UnlockBits(bitmapData);

                // Crop image to 206x156
                croppedBitmap = cropFilter.Apply(bitmap);

                // Upscale 200%
                bigBitmap = resizeFilter.Apply(croppedBitmap);

                // Sharpen image
                if (sharpenImage) sharpenFilter.ApplyInPlace(bigBitmap);

                UpdateAnalyzablePictureBox(livePicture, bigBitmap, arrID3);

                if (recorder != null)
                    recorder.SupplyFrame(bigBitmap);

                if (firstAfterCal)
                {
                    firstAfterCal = false;
                    UpdateAnalyzablePictureBox(firstAfterCalPicture, bigBitmap, arrID3);

                    if (autoSaveImg)
                        Output.Screenshot(bigBitmap, outputPathField.Text);
                }

                DrawHistogram();
            }

            UpdateAnalyzablePictureBoxSize(livePicture);
            UpdateAnalyzablePictureBoxSize(firstAfterCalPicture);
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            stopThread = true;

            if (thermal != null)
            {
                thermalThread.Join(500);
                thermal.Deinit();
            }
        }

        class ComboItem
        {
            public string Key { get; set; }
            public string Value { get; set; }

            public ComboItem(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }

        private void HandlePaletteComboSelectedIndexChanged(object sender, EventArgs e)
        {
            ComboItem newPal = (ComboItem) paletteCombo.SelectedItem;
            Bitmap paletteImg = new Bitmap(newPal.Key);

            for (int i = 0; i < 1001; i++)
            {
                Color picColor = paletteImg.GetPixel(i, 0);
                paletteArr[i, 0] = picColor.R;
                paletteArr[i, 1] = picColor.G;
                paletteArr[i, 2] = picColor.B;
            }

            paletteImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
            tempGaugePicture.Image = paletteImg;
        }

        private void HandleUnitRadiosCheckedChanged(object sender, EventArgs e)
        {
            if (unitsKRadio.Checked) tempUnit = "K";
            if (unitsCRadio.Checked) tempUnit = "C";
            if (unitsFRadio.Checked) tempUnit = "F";

            livePicture.TempUnit = tempUnit;
            firstAfterCalPicture.TempUnit = tempUnit;
        }

        private void HandleManualRangeSwitchButtonClick(object sender, EventArgs e)
        {
            autoRange = !autoRange;

            if (autoRange)
            {
                manualRangeSwitchButton.Text = "Switch to manual range";
                dynSlidersCheck.Checked = false;
                dynSlidersCheck.Visible = false;
            }
            else
            {
                manualRangeSwitchButton.Text = "Switch to auto range";
                dynSlidersCheck.Visible = true;
            }
        }

        private void HandleDynSlidersCheckCheckedChanged(object sender, EventArgs e)
        {
            dynSliders = !dynSliders;

            int currentLeftPos = minTempSlider.Value;
            int currentRightPos = maxTempSlider.Value;
            int currentDiff = currentRightPos - currentLeftPos;

            if (dynSliders)
            {
                // Left min
                if (currentLeftPos - currentDiff > 4000)
                    minTempSlider.Minimum = currentLeftPos - currentDiff;
                else
                    minTempSlider.Minimum = 4000;

                // Left max
                if (currentLeftPos + currentDiff * 2 < 20000)
                    minTempSlider.Maximum = currentLeftPos + currentDiff * 2;
                else
                    minTempSlider.Maximum = 20000;

                // Right min
                if (currentRightPos - currentDiff * 2 > 4000)
                    maxTempSlider.Minimum = currentRightPos - currentDiff * 2;
                else
                    maxTempSlider.Minimum = 4000;

                // Right max
                if (currentRightPos + currentDiff < 20000)
                    maxTempSlider.Maximum = currentRightPos + currentDiff;
                else
                    maxTempSlider.Maximum = 20000;
            }
            else
            {
                maxTempSlider.Minimum = 4000;
                minTempSlider.Minimum = 4000;
                maxTempSlider.Maximum = 20000;
                minTempSlider.Maximum = 20000;
            }
        }

        private void HandleAnalysisCheckCheckedChanged(object sender, EventArgs e)
        {
            livePicture.AnalysisEnabled = enableAnalysisCheck.Checked;
            firstAfterCalPicture.AnalysisEnabled = enableAnalysisCheck.Checked;
        }

        private void HandleShowTemperatureCheckCheckedChanged(object sender, EventArgs e)
        {
            livePicture.ShowTemperature = showTemperatureCheck.Checked;
            firstAfterCalPicture.ShowTemperature = showTemperatureCheck.Checked;
        }

        private void HandleCrossSizeSpinnerValueChanged(object sender, EventArgs e)
        {
            livePicture.CrossSize = (int) crossSizeSpinner.Value;
            firstAfterCalPicture.CrossSize = (int) crossSizeSpinner.Value;
        }

        private void HandleShowExtremesCheckCheckedChanged(object sender, EventArgs e)
        {
            livePicture.ShowExtremes = showExtremesCheck.Checked;
            firstAfterCalPicture.ShowExtremes = showExtremesCheck.Checked;
        }

        private void HandleMaxCountSpinnerValueChanged(object sender, EventArgs e)
        {
            livePicture.MaxCount = (int) maxCountSpinner.Value;
            firstAfterCalPicture.MaxCount = (int) maxCountSpinner.Value;
        }

        private void HandleDeletePointsButtonClick(object sender, EventArgs e)
        {
            if (pictureTabs.SelectedTab == liveTab)
                livePicture.DeleteAllAnalyzers();
            else if (pictureTabs.SelectedTab == firstAfterCalTab)
                firstAfterCalPicture.DeleteAllAnalyzers();
        }

        private void HandleOutputPathButtonClick(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    outputPathField.Text = dialog.SelectedPath;
                }
            }
        }

        private void HandleScreenshotButtonClick(object sender, EventArgs e)
        {
            Output.Screenshot(bigBitmap, outputPathField.Text);
        }

        private void HandleRecordVideoButtonClick(object sender, EventArgs e)
        {
            if (recorder == null)
            {
                recorder = new Output.VideoRecorder(outputPathField.Text);

                if (!recorder.Start())
                {
                    recorder = null;
                    return;
                }

                recordVideoButton.Text = "Stop recording";
            }
            else
            {
                recorder.Stop();
                recorder = null;

                recordVideoButton.Text = "Record video";
            }
        }

        private void HandleAnalyzablePictureMouseEnter(object sender, EventArgs e)
        {
            mouseLabel.Visible = true;
        }

        private void HandleAnalyzablePictureMouseLeave(object sender, EventArgs e)
        {
            mouseLabel.Visible = false;
        }

        private void HandleAnalyzablePictureMouseMove(object sender, MouseEventArgs e)
        {
            var picture = (AnalyzablePictureBox) sender;
            var coords = picture.LocalToCoords(e.Location);
            mouseLabel.Text = $"({coords.X}, {coords.Y}): {Utils.FormatTempString(tempUnit, picture.DetectTemperature(coords))}";
        }

        private void HandleMinTempSliderScroll(object sender, EventArgs e)
        {
            if (autoRange) return;

            if (minTempSlider.Value < maxTempSlider.Value - 10)
            {
                gModeLeftManual = (ushort) minTempSlider.Value;
            }
            else
            {
                minTempSlider.Value = maxTempSlider.Value - 10;
            }
        }

        private void HandleMaxTempSliderScroll(object sender, EventArgs e)
        {
            if (autoRange) return;

            if (maxTempSlider.Value > minTempSlider.Value + 10)
            {
                gModeRightManual = (ushort) maxTempSlider.Value;
            }
            else
            {
                maxTempSlider.Value = minTempSlider.Value + 10;
            }
        }
    }
}
