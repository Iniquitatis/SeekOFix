/*
Copyright (c) 2014 Stephen Stair (sgstair@akkit.org)
Additional code Miguel Parra (miguelvp@msn.com)
Additional code by Franci Kapel: http://html-color-codes.info/Contact/
(Franci Kapel: A lot of my code was influenced by JadeW's work:  https://github.com/rzva/ThermalView)

Permission is hereby granted, free of charge, to any person obtaining a copy
 of this software and associated documentation files (the "Software"), to deal
 in the Software without restriction, including without limitation the rights
 to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 copies of the Software, and to permit persons to whom the Software is
 furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
 all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using winusbdotnet.UsbDevices;

namespace TestSeek
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

        string localPath;
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

        public MainWindow()
        {
            InitializeComponent();

            localPath = Directory.GetCurrentDirectory().ToString();
            Directory.CreateDirectory(localPath + @"\export");

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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            var pngFiles = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\palette").GetFiles("*.png");

            foreach (FileInfo file in pngFiles)
            {
                paletteCombo.Items.Add(new ComboItem(file.FullName, file.Name.Replace(".png", "")));
            }

            paletteCombo.SelectedIndex = 1;
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            if (lastUsableFrame != null)
            {
                string minTemp = Utils.FormatTempString(tempUnit, gModeLeft);
                string maxTemp = Utils.FormatTempString(tempUnit, gModeRight);

                minTempLabel.Text = minTemp;
                maxTempLabel.Text = maxTemp;
                sliderMinLabel.Text = minTemp;
                sliderMaxLabel.Text = maxTemp;

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

                if (firstAfterCal)
                {
                    firstAfterCal = false;
                    UpdateAnalyzablePictureBox(firstAfterCalPicture, bigBitmap, arrID3);

                    if (autoSaveImg)
                        bigBitmap.Save(localPath + @"\export\seek_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss_fff") + ".png");
                }

                DrawHistogram();
            }

            UpdateAnalyzablePictureBoxSize(livePicture);
            UpdateAnalyzablePictureBoxSize(firstAfterCalPicture);
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopThread = true;

            if (thermal != null)
            {
                thermalThread.Join(500);
                thermal.Deinit();
            }
        }

        private void startStopButton_Click(object sender, EventArgs e)
        {
            if (isRunning)
            {
                thermalThread.Suspend();
                startStopButton.Text = "START";
            }
            else
            {
                thermalThread.Resume();
                startStopButton.Text = "STOP";
            }

            isRunning = !isRunning;
        }

        private void intCalButton_Click(object sender, EventArgs e)
        {
            usingExternalCal = false;
        }

        private void extCalButton_Click(object sender, EventArgs e)
        {
            grabExternalReference = true;
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

        private void paletteCombo_SelectedIndexChanged(object sender, EventArgs e)
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
            temperatureGaugePicture.Image = paletteImg;
        }

        private void autoSaveCheck_CheckedChanged(object sender, EventArgs e)
        {
            autoSaveImg = !autoSaveImg;
        }

        private void applySharpenCheck_CheckedChanged(object sender, EventArgs e)
        {
            sharpenImage = !sharpenImage;
        }

        private void unitRadios_CheckedChanged(object sender, EventArgs e)
        {
            if (unitsKRadio.Checked) tempUnit = "K";
            if (unitsCRadio.Checked) tempUnit = "C";
            if (unitsFRadio.Checked) tempUnit = "F";

            livePicture.TempUnit = tempUnit;
            firstAfterCalPicture.TempUnit = tempUnit;
        }

        private void manualRangeSwitchButton_Click(object sender, EventArgs e)
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

        private void dynSlidersCheck_CheckedChanged(object sender, EventArgs e)
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

        private void analysisCheck_CheckedChanged(object sender, EventArgs e)
        {
            livePicture.AnalysisEnabled = enableAnalysisCheck.Checked;
            firstAfterCalPicture.AnalysisEnabled = enableAnalysisCheck.Checked;
        }

        private void showTemperatureCheck_CheckedChanged(object sender, EventArgs e)
        {
            livePicture.ShowTemperature = showTemperatureCheck.Checked;
            firstAfterCalPicture.ShowTemperature = showTemperatureCheck.Checked;
        }

        private void crossSizeSpinner_ValueChanged(object sender, EventArgs e)
        {
            livePicture.CrossSize = (int) crossSizeSpinner.Value;
            firstAfterCalPicture.CrossSize = (int) crossSizeSpinner.Value;
        }

        private void showExtremesCheck_CheckedChanged(object sender, EventArgs e)
        {
            livePicture.ShowExtremes = showExtremesCheck.Checked;
            firstAfterCalPicture.ShowExtremes = showExtremesCheck.Checked;
        }

        private void maxCountSpinner_ValueChanged(object sender, EventArgs e)
        {
            livePicture.MaxCount = (int) maxCountSpinner.Value;
            firstAfterCalPicture.MaxCount = (int) maxCountSpinner.Value;
        }

        private void thermoPicture_MouseEnter(object sender, EventArgs e)
        {
            mouseLabel.Visible = true;
        }

        private void thermoPicture_MouseLeave(object sender, EventArgs e)
        {
            mouseLabel.Visible = false;
        }

        private void thermoPicture_MouseMove(object sender, MouseEventArgs e)
        {
            var picture = (AnalyzablePictureBox) sender;
            mouseLabel.Text = picture.LocalToCoords(e.Location).ToString();
        }

        private void minTempSlider_Scroll(object sender, EventArgs e)
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

        private void maxTempSlider_Scroll(object sender, EventArgs e)
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

    public static class Constants
    {
        public const int DATA_W = 208;
        public const int DATA_H = 156;
        public const int DATA_LENGTH = DATA_W * DATA_H;
        public const int IMAGE_W = DATA_W - 2;
        public const int IMAGE_H = DATA_H;
    }

    public class AnalyzablePictureBox : PictureBox
    {
        private bool _analysisEnabled = false;
        private string _tempUnit = "K";
        private bool _showTemperature = true;
        private int _crossSize = 16;
        private bool _showExtremes = true;
        private int _maxCount = 3;
        private ushort[] _rawValues = null;
        private List<Analyzer> _analyzers = new List<Analyzer>();
        private Analyzer _hotAnalyzer = new Analyzer();
        private Analyzer _coldAnalyzer = new Analyzer();

        public bool AnalysisEnabled
        {
            get { return _analysisEnabled; }
            set { _analysisEnabled = value; Reanalyze(); }
        }

        public string TempUnit
        {
            get { return _tempUnit; }
            set { _tempUnit = value; Invalidate(); }
        }

        public bool ShowTemperature
        {
            get { return _showTemperature; }
            set { _showTemperature = value; Invalidate(); }
        }

        public int CrossSize
        {
            get { return _crossSize; }
            set { _crossSize = value; Invalidate(); }
        }

        public bool ShowExtremes
        {
            get { return _showExtremes;  }
            set { _showExtremes = value; Reanalyze(); }
        }

        public int MaxCount
        {
            get { return _maxCount; }
            set { _maxCount = value; AdjustAnalyzerCount(); Invalidate(); }
        }

        public ushort[] RawValues
        {
            get { return _rawValues; }
            set { _rawValues = value; Reanalyze(); }
        }

        public AnalyzablePictureBox()
        {
            MouseDown += HandleMouseDown;
            Paint += HandlePaint;
        }

        public void Reanalyze()
        {
            if (!_analysisEnabled) return;

            DetectExtremes();

            foreach (var analyzer in _analyzers)
            {
                analyzer.temperature = DetectTemperature(analyzer.coords);
            }

            Invalidate();
        }

        public Point LocalToCoords(Point p)
        {
            var pw = (double) Width;
            var ph = (double) Height;
            var iw = (double) Constants.IMAGE_W;
            var ih = (double) Constants.IMAGE_H;

            var sx = iw / pw;
            var sy = ih / ph;

            return new Point((int) ((double) p.X * sx), (int) ((double) p.Y * sy));
        }

        public Point CoordsToLocal(Point p)
        {
            var pw = (double) Width;
            var ph = (double) Height;
            var iw = (double) Constants.IMAGE_W;
            var ih = (double) Constants.IMAGE_H;

            var sx = pw / iw;
            var sy = ph / ih;

            return new Point((int) ((double) p.X * sx), (int) ((double) p.Y * sy));
        }

        public Size PixelSize()
        {
            var pw = (double) Width;
            var ph = (double) Height;
            var iw = (double) Constants.IMAGE_W;
            var ih = (double) Constants.IMAGE_H;

            var sx = pw / iw;
            var sy = ph / ih;

            return new Size((int) sx, (int) sy);
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (!_analysisEnabled) return;

            if (e.Button == MouseButtons.Left)
            {
                var coords = LocalToCoords(e.Location);
                _analyzers.Add(new Analyzer(coords, DetectTemperature(coords)));

                AdjustAnalyzerCount();
            }
            else if (e.Button == MouseButtons.Right)
            {
                var size = new Size(_crossSize, _crossSize);
                var cursor = e.Location;
                cursor.Offset(new Point(_crossSize / 2, _crossSize / 2));
                _analyzers.RemoveAll(x => new Rectangle(CoordsToLocal(x.coords), size).Contains(cursor));
            }

            Invalidate();
        }

        private void HandlePaint(object sender, PaintEventArgs e)
        {
            if (!_analysisEnabled) return;

            var g = e.Graphics;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            foreach (var analyzer in _analyzers)
            {
                DrawAnalyzer(g, analyzer, Color.White, Color.White);
            }

            if (_showExtremes)
            {
                DrawAnalyzer(g, _hotAnalyzer, Color.Orange, Color.White);
                DrawAnalyzer(g, _coldAnalyzer, Color.LightBlue, Color.White);
            }

            g.TextRenderingHint = TextRenderingHint.SystemDefault;
            g.SmoothingMode = SmoothingMode.None;
            g.InterpolationMode = InterpolationMode.Default;
            g.CompositingQuality = CompositingQuality.Default;
        }

        private void DrawAnalyzer(Graphics graphics, Analyzer analyzer, Color color, Color textColor)
        {
            var local = CoordsToLocal(analyzer.coords);
            var pixelSize = PixelSize();
            var x = local.X + pixelSize.Width / 2;
            var y = local.Y + pixelSize.Height / 2;
            var hw = _crossSize / 2;
            var hh = _crossSize / 2;
            var t = analyzer.temperature;

            var lineOutline = new Pen(Color.Black, 5.0f);
            lineOutline.LineJoin = LineJoin.Round;

            var lineFill = new Pen(color, 1.0f);
            lineFill.LineJoin = LineJoin.Round;

            var crossPath = new GraphicsPath();
            crossPath.AddLine(x - hw, y, x + hw, y);
            crossPath.CloseFigure();
            crossPath.AddLine(x, y - hh, x, y + hh);
            crossPath.CloseFigure();

            var g = graphics;
            g.DrawPath(lineOutline, crossPath);
            g.DrawPath(lineFill, crossPath);

            if (_showTemperature)
            {
                var textOutline = new Pen(Color.Black, 4.0f);
                textOutline.LineJoin = LineJoin.Round;

                var textFill = new SolidBrush(textColor);

                var font = new FontFamily("Segoe UI");

                var textFormat = new StringFormat();
                textFormat.Alignment = StringAlignment.Center;
                textFormat.LineAlignment = StringAlignment.Far;

                var textPath = new GraphicsPath();
                textPath.AddString(Utils.FormatTempString(_tempUnit, t), font, (int) FontStyle.Regular, 18.0f, new Point(x, y - hh), textFormat);

                g.DrawPath(textOutline, textPath);
                g.FillPath(textFill, textPath);

                textPath.Dispose();
                textFormat.Dispose();
                font.Dispose();
                textFill.Dispose();
                textOutline.Dispose();
            }

            crossPath.Dispose();
            lineFill.Dispose();
            lineOutline.Dispose();
        }

        private void DetectExtremes()
        {
            if (_rawValues == null) return;

            var lowest = 30000;
            var highest = 0;

            var lx = 0;
            var ly = 0;
            var hx = 0;
            var hy = 0;

            for (var y = 0; y < Constants.IMAGE_H; y++)
            {
                for (var x = 0; x < Constants.IMAGE_W; x++)
                {
                    var value = _rawValues[y * Constants.DATA_W + x];

                    if (value < lowest)
                    {
                        lowest = value;
                        lx = x;
                        ly = y;
                    }

                    if (value > highest)
                    {
                        highest = value;
                        hx = x;
                        hy = y;
                    }
                }
            }

            Console.WriteLine($"EXT {lowest}-{highest} in {lx},{ly};{hx},{hy}");

            _coldAnalyzer.coords.X = lx;
            _coldAnalyzer.coords.Y = ly;
            _coldAnalyzer.temperature = lowest;

            _hotAnalyzer.coords.X = hx;
            _hotAnalyzer.coords.Y = hy;
            _hotAnalyzer.temperature = highest;
        }

        private int DetectTemperature(Point coords)
        {
            if (_rawValues == null) return 0;
            Console.WriteLine($"TEMP {_rawValues[coords.Y * Constants.DATA_W + coords.X]}");
            return _rawValues[coords.Y * Constants.DATA_W + coords.X];
        }

        private void AdjustAnalyzerCount()
        {
            while (_analyzers.Count > _maxCount) 
                _analyzers.RemoveAt(0);
        }
    }

    public class Analyzer
    {
        public Point coords = new Point();
        public int temperature = 0;

        public Analyzer()
        {

        }

        public Analyzer(Point coords, int temperature)
        {
            this.coords = coords;
            this.temperature = temperature;
        }
    }

    public static class Utils
    {
        public static string FormatTempString(string unit, int rawValue)
        {
            return $"{RawToTemp(unit, rawValue):0.0} {GetTempSymbol(unit)}";
        }

        public static string GetTempSymbol(string unit)
        {
            switch (unit)
            {
                case "K": return "K";
                case "C": return "°C";
                case "F": return "°F";
                default: return "";
            }
        }

        public static double RawToTemp(string unit, int rawValue)
        {
            var tempValue = ((double) rawValue - 5950.0) / 40.0 + 273.15;

            switch (unit)
            {
                case "K": return tempValue;
                case "C": return tempValue - 273.15;
                case "F": return tempValue * 9.0 / 5.0 - 459.67;
                default: return 0.0;
            }
        }
    }
}
