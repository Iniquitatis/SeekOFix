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
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
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
        string tempUnit;

        ushort[] arrID4 = new ushort[32448];
        ushort[] arrID1 = new ushort[32448];
        ushort[] arrID3 = new ushort[32448];

        bool[] badPixelArr = new bool[32448];

        ushort[] gMode = new ushort[1000];

        ushort[,] paletteArr = new ushort[1001, 3]; //-> ushort

        byte[] imgBuffer = new byte[97344];

        ushort gModePeakIx = 0;
        ushort gModePeakCnt = 0;
        ushort gModeLeft = 0;
        ushort gModeRight = 0;
        ushort gModeLeftManual = 0;
        ushort gModeRightManual = 0;
        ushort avgID4 = 0;
        ushort avgID1 = 0;
        ushort maxTempRaw = 0;

        double[] gainCalArr = new double[32448];

        Bitmap bitmap = new Bitmap(208, 156, PixelFormat.Format24bppRgb);
        Bitmap croppedBitmap = new Bitmap(206, 156, PixelFormat.Format24bppRgb);
        Bitmap bigBitmap = new Bitmap(412, 312, PixelFormat.Format24bppRgb);
        BitmapData bitmap_data;

        //ResizeBicubic bicubicResize = new ResizeBicubic(412, 312);
        ResizeBilinear bilinearResize = new ResizeBilinear(412, 312);
        Crop cropFilter = new Crop(new Rectangle(0, 0, 206, 156));
        Sharpen sharpenFilter = new Sharpen();

        public MainWindow()
        {
            InitializeComponent();

            localPath = Directory.GetCurrentDirectory().ToString();
            Directory.CreateDirectory(localPath + @"\export");

            unitsKRadio.CheckedChanged += new EventHandler(unitRadios_CheckedChanged);
            unitsCRadio.CheckedChanged += new EventHandler(unitRadios_CheckedChanged);
            unitsFRadio.CheckedChanged += new EventHandler(unitRadios_CheckedChanged);

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
                        if (!usingExternalCal) Frame1Stuff();
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

            for (int i = 0; i < 32448; i++)
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

            for (int i = 0; i < 32448; i++)
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
            double iScaler;
            iScaler = (double) (gModeRight - gModeLeft) / 1000;

            for (int i = 0; i < 32448; i++)
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

            for (ushort y = 0; y < 156; y++)
            {
                for (ushort x = 0; x < 208; x++, i++)
                {
                    if (badPixelArr[i] && x < 206)
                    {
                        ushort val = 0;
                        ushort nr = 0;

                        // Top pixel
                        if (y > 0 && !badPixelArr[i - 208])
                        {
                            val += arrID3[i - 208];
                            ++nr;
                        }

                        // Bottom pixel
                        if (y < 155 && !badPixelArr[i + 208])
                        {
                            val += arrID3[i + 208];
                            ++nr;
                        }

                        // Left pixel
                        if (x > 0 && !badPixelArr[i - 1])
                        {
                            val += arrID3[i - 1];
                            ++nr;
                        }

                        // Right pixel
                        if (x < 205 && !badPixelArr[i + 1])
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
                }
            }
        }

        private void RemoveNoise()
        {
            ushort x = 0;
            ushort y = 0;
            ushort i = 0;
            ushort val = 0;
            ushort[] arrColor = new ushort[4];

            for (y = 0; y < 156; y++)
            {
                for (x = 0; x < 208; x++)
                {
                    if (x > 0 && x < 206 && y > 0 && y < 155)
                    {
                        arrColor[0] = arrID3[i - 208]; // Top
                        arrColor[1] = arrID3[i + 208]; // Bottom
                        arrColor[2] = arrID3[i - 1]; // Left
                        arrColor[3] = arrID3[i + 1]; // Right

                        val = (ushort) ((arrColor[0] + arrColor[1] + arrColor[2] + arrColor[3] - Highest(arrColor) - Lowest(arrColor)) / 2);

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

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopThread = true;
            if (thermal != null)
            {
                thermalThread.Join(500);
                thermal.Deinit();
            }
        }

        // Button to capture external reference or switch to internal shutter
        private void extCalButton_Click(object sender, EventArgs e)
        {
            grabExternalReference = true;
        }

        // Button to toggle between automatic ranging or manual
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

        private void maxTempSlider_Scroll(object sender, EventArgs e)
        {
            if (!autoRange)
            {
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

        private void minTempSlider_Scroll(object sender, EventArgs e)
        {
            if (!autoRange)
            {
                if (minTempSlider.Value < maxTempSlider.Value - 10)
                {
                    gModeLeftManual = (ushort) minTempSlider.Value;
                }
                else
                {
                    minTempSlider.Value = maxTempSlider.Value - 10;
                }
            }
        }

        private void autoSaveCheck_CheckedChanged(object sender, EventArgs e)
        {
            autoSaveImg = !autoSaveImg;
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
                if (currentLeftPos - currentDiff > 4000) minTempSlider.Minimum = currentLeftPos - currentDiff;
                else minTempSlider.Minimum = 4000;

                // Left max
                if (currentLeftPos + currentDiff * 2 < 20000) minTempSlider.Maximum = currentLeftPos + currentDiff * 2;
                else minTempSlider.Maximum = 20000;

                // Right min
                if (currentRightPos - currentDiff * 2 > 4000) maxTempSlider.Minimum = currentRightPos - currentDiff * 2;
                else maxTempSlider.Minimum = 4000;

                // Right max
                if (currentRightPos + currentDiff < 20000) maxTempSlider.Maximum = currentRightPos + currentDiff;
                else maxTempSlider.Maximum = 20000;
            }
            else
            {
                maxTempSlider.Minimum = 4000;
                minTempSlider.Minimum = 4000;
                maxTempSlider.Maximum = 20000;
                minTempSlider.Maximum = 20000;
            }
        }

        private void applySharpenCheck_CheckedChanged(object sender, EventArgs e)
        {
            sharpenImage = !sharpenImage;
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

        private void paletteCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboItem newPal = (ComboItem) paletteCombo.SelectedItem;
            Bitmap paletteImg = new Bitmap(newPal.Key);
            Color picColor;

            for (int i = 0; i < 1001; i++)
            {
                picColor = paletteImg.GetPixel(i, 0);
                paletteArr[i, 0] = picColor.R;
                paletteArr[i, 1] = picColor.G;
                paletteArr[i, 2] = picColor.B;
            }

            paletteImg.RotateFlip(RotateFlipType.Rotate270FlipNone);
            temperatureGaugePicture.Image = paletteImg;
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

        public ushort GetMode(ushort[] arr)
        {
            ushort[] arrMode = new ushort[320];
            ushort topPos = 0;
            for (ushort i = 0; i < 32448; i++)
            {
                if ((arr[i] > 1000) && (arr[i] / 100 != 0)) arrMode[(arr[i]) / 100]++;
            }

            topPos = (ushort) Array.IndexOf(arrMode, arrMode.Max());

            return (ushort) (topPos * 100);
        }

        public void GetHistogram()
        {
            maxTempRaw = arrID3.Max();
            ushort[] arrMode = new ushort[2100];
            ushort topPos = 0;
            for (ushort i = 0; i < 32448; i++)
            {
                if ((arrID3[i] > 1000) && (arrID3[i] / 10 != 0) && !badPixelArr[i]) arrMode[(arrID3[i]) / 10]++;
            }

            topPos = (ushort) Array.IndexOf(arrMode, arrMode.Max());

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

        private string RawToTemp(int val)
        {
            double tempVal = 0;

            tempVal = (double) (val - 5950) / 40 + 273.15; // K

            switch (tempUnit)
            {
                case "C":
                    tempVal = tempVal - 273.15; // C
                    break;

                case "F":
                    tempVal = tempVal * 9 / 5 - 459.67; // F
                    break;
            }

            return tempVal.ToString("F1", CultureInfo.InvariantCulture);
        }

        private void intCalButton_Click(object sender, EventArgs e)
        {
            usingExternalCal = false;
        }

        private void unitRadios_CheckedChanged(object sender, EventArgs e)
        {
            if (unitsKRadio.Checked) tempUnit = "K";
            if (unitsCRadio.Checked) tempUnit = "C";
            if (unitsFRadio.Checked) tempUnit = "F";
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            if (lastUsableFrame != null)
            {
                string minTemp = RawToTemp(gModeLeft);
                string maxTemp = RawToTemp(gModeRight);

                sliderMinLabel.Text = minTemp;
                sliderMaxLabel.Text = maxTemp;
                minTempLabel.Text = minTemp;
                maxTempLabel.Text = maxTemp;

                gModeLeftLabel.Text = gModeLeft.ToString();
                gModeRightLabel.Text = gModeRight.ToString();
                maxTempRawLabel.Text = maxTempRaw.ToString();

                // Set sliders position
                if (autoRange)
                {
                    minTempSlider.Value = gModeLeft;
                    maxTempSlider.Value = gModeRight;
                }

                bitmap_data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                Marshal.Copy(imgBuffer, 0, bitmap_data.Scan0, imgBuffer.Length);
                bitmap.UnlockBits(bitmap_data);

                // Crop image to 206x156
                croppedBitmap = cropFilter.Apply(bitmap);

                // Upscale 200%
                bigBitmap = bilinearResize.Apply(croppedBitmap);

                // Sharpen image
                if (sharpenImage) sharpenFilter.ApplyInPlace(bigBitmap);

                livePicture.Image = bigBitmap;

                if (firstAfterCal)
                {
                    firstAfterCal = false;
                    firstAfterCalPicture.Image = bigBitmap;
                    if (autoSaveImg) bigBitmap.Save(localPath + @"\export\seek_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss_fff") + ".png");
                }

                DrawHistogram();
            }

        }

    }
}
