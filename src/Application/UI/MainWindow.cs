using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using winusbdotnet.UsbDevices;
using SeekOFix.Common;

namespace SeekOFix.UI
{
    public partial class MainWindow : Form
    {
        private SeekThermal _thermal = null;
        private Thread _thermalThread = null;
        private ThermalFrame _currentFrame = null;
        private ThermalFrame _lastUsableFrame = null;

        private bool _stopThread = false;
        private bool _grabExternalReference = false;
        private bool _firstAfterCal = false;
        private bool _usingExternalCal = false;
        private bool _autoRange = true;
        private bool _isRunning = true;

        private TemperatureUnit _tempUnit = TemperatureUnit.K;

        private ushort[] _arrID4 = new ushort[Constants.DATA_LENGTH];
        private ushort[] _arrID1 = new ushort[Constants.DATA_LENGTH];
        private ushort[] _arrID3 = new ushort[Constants.DATA_LENGTH];

        private bool[] _badPixelArr = new bool[Constants.DATA_LENGTH];

        private ushort[] _gMode = new ushort[Constants.HISTOGRAM_SIZE];

        private byte[,] _palette = new byte[Constants.PALETTE_SIZE, 3];

        private ushort _gModePeakIx = 0;
        private ushort _gModePeakCnt = 0;
        private ushort _gModeLeft = 0;
        private ushort _gModeRight = 0;
        private ushort _gModeLeftManual = 0;
        private ushort _gModeRightManual = 0;
        private ushort _avgID4 = 0;
        private ushort _avgID1 = 0;
        private ushort _maxTempRaw = 0;

        private double[] _gainCalArr = new double[Constants.DATA_LENGTH];

        private Output.VideoRecorder _recorder = null;

        private FrameIO.Reader _reader = null;
        private FrameIO.Writer _writer = null;

        public MainWindow(string mode, string ioPath, int maxFrames)
        {
            InitializeComponent();

            var localPath = Directory.GetCurrentDirectory().ToString();
            var exportPath = localPath + @"\Export";
            Directory.CreateDirectory(exportPath);
            _outputPathField.Text = exportPath;

            if (mode == "stream" || mode == "write")
            {
                var device = SeekThermal.Enumerate().FirstOrDefault();

                if (device == null)
                {
                    MessageBox.Show("No Seek Thermal devices found.");
                    return;
                }

                _thermal = new SeekThermal(device);

                if (mode == "write")
                {
                    _writer = new FrameIO.Writer(ioPath, maxFrames);
                }
            }
            else if (mode == "read")
            {
                _reader = new FrameIO.Reader(ioPath);
            }

            _thermalThread = new Thread(ThermalThreadProc);
            _thermalThread.IsBackground = true;
            _thermalThread.Start();
        }

        private void ToggleThreadActivity()
        {
            if (_isRunning)
                _thermalThread.Suspend();
            else
                _thermalThread.Resume();

            _isRunning = !_isRunning;
        }

        private void ThermalThreadProc()
        {
            while (!_stopThread && (_thermal != null || _reader != null))
            {
                bool progress = false;

                _currentFrame = GetFrame();
                RecordFrame(_currentFrame);

                switch (_currentFrame.StatusByte)
                {
                    // Gain calibration
                    case 4:
                        Frame4Stuff();

                        break;

                    // Shutter calibration
                    case 1:
                        MarkBadPixels();

                        if (!_usingExternalCal)
                            Frame1Stuff();

                        _firstAfterCal = true;

                        break;

                    // Image frame
                    case 3:
                        MarkBadPixels();

                        // Use this image as reference
                        if (_grabExternalReference)
                        {
                            _grabExternalReference = false;
                            _usingExternalCal = true;
                            Frame1Stuff();
                        }
                        else
                        {
                            Frame3Stuff();
                            _lastUsableFrame = _currentFrame;
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

        private ThermalFrame GetFrame()
        {
            if (_thermal != null)
            {
                return _thermal.GetFrameBlocking();
            }
            else
            {
                Thread.Sleep(100);
                return _reader.Read();
            }
        }

        private void RecordFrame(ThermalFrame frame)
        {
            if (_writer == null) return;
            _writer.Write(frame);
        }

        private void Frame4Stuff()
        {
            _arrID4 = _currentFrame.RawDataU16;
            _avgID4 = GetMode(_arrID4);

            for (int i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if (_arrID4[i] > 2000 && _arrID4[i] < 8000)
                {
                    _gainCalArr[i] = _avgID4 / (double) _arrID4[i];
                }
                else
                {
                    _gainCalArr[i] = 1;
                    _badPixelArr[i] = true;
                }
            }
        }

        private void Frame1Stuff()
        {
            _arrID1 = _currentFrame.RawDataU16;
            //_avgID1 = GetMode(_arrID1);
        }

        private void Frame3Stuff()
        {
            _arrID3 = _currentFrame.RawDataU16;

            for (int i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if (_arrID3[i] > 2000)
                {
                    _arrID3[i] = (ushort) ((_arrID3[i] - _arrID1[i]) * _gainCalArr[i] + 7500);
                }
                else
                {
                    _arrID3[i] = 0;
                    _badPixelArr[i] = true;
                }
            }

            FixBadPixels();
            RemoveNoise();
            GetHistogram();
        }

        private void MarkBadPixels()
        {
            ushort[] rawDataArr = _currentFrame.RawDataU16;

            for (int i = 0; i < rawDataArr.Length; i++)
            {
                if (rawDataArr[i] < 2000 || rawDataArr[i] > 22000)
                {
                    _badPixelArr[i] = true;
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
                    if (_badPixelArr[i] && x < Constants.IMAGE_W)
                    {
                        ushort val = 0;
                        ushort nr = 0;

                        // Top pixel
                        if (y > 0 && !_badPixelArr[i - Constants.DATA_W])
                        {
                            val += _arrID3[i - Constants.DATA_W];
                            ++nr;
                        }

                        // Bottom pixel
                        if (y < (Constants.IMAGE_H - 1) && !_badPixelArr[i + Constants.DATA_W])
                        {
                            val += _arrID3[i + Constants.DATA_W];
                            ++nr;
                        }

                        // Left pixel
                        if (x > 0 && !_badPixelArr[i - 1])
                        {
                            val += _arrID3[i - 1];
                            ++nr;
                        }

                        // Right pixel
                        if (x < (Constants.IMAGE_W - 1) && !_badPixelArr[i + 1])
                        {
                            val += _arrID3[i + 1];
                            ++nr;
                        }

                        if (nr > 0)
                        {
                            val /= nr;
                            _arrID3[i] = val;
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
                        arrColor[0] = _arrID3[i - Constants.DATA_W]; // Top
                        arrColor[1] = _arrID3[i + Constants.DATA_W]; // Bottom
                        arrColor[2] = _arrID3[i - 1]; // Left
                        arrColor[3] = _arrID3[i + 1]; // Right

                        ushort val = (ushort) ((arrColor[0] + arrColor[1] + arrColor[2] + arrColor[3] - Highest(arrColor) - Lowest(arrColor)) / 2);

                        if (Math.Abs(val - _arrID3[i]) > 100 && val != 0)
                        {
                            _arrID3[i] = val;
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

        private ushort GetMode(ushort[] arr)
        {
            ushort[] arrMode = new ushort[320];

            for (ushort i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if ((arr[i] > 1000) && (arr[i] / 100 != 0)) arrMode[arr[i] / 100]++;
            }

            ushort topPos = (ushort) Array.IndexOf(arrMode, arrMode.Max());

            return (ushort) (topPos * 100);
        }

        private void GetHistogram()
        {
            _maxTempRaw = _arrID3.Max();

            for (ushort i = 0; i < Constants.DATA_LENGTH; i++)
            {
                if ((_arrID3[i] > 1000) && (_arrID3[i] / 10 != 0) && !_badPixelArr[i]) _gMode[_arrID3[i] / 10]++;
            }

            ushort topPos = (ushort) Array.IndexOf(_gMode, _gMode.Max());

            _gModePeakCnt = _gMode.Max();
            _gModePeakIx = (ushort) (topPos * 10);

            // Lower it to 100px
            for (ushort i = 0; i < Constants.HISTOGRAM_SIZE; i++)
            {
                _gMode[i] = (ushort) ((double) _gMode[i] / _gModePeakCnt * 100);
            }

            if (_autoRange)
            {
                _gModeLeft = _gModePeakIx;
                _gModeRight = _gModePeakIx;

                // Find left border
                for (ushort i = 0; i < topPos; i++)
                {
                    if (_gMode[i] > _gMode[topPos] * 0.01)
                    {
                        _gModeLeft = (ushort) (i * 10);
                        break;
                    }
                }

                // Find right border
                for (ushort i = (Constants.HISTOGRAM_SIZE - 1); i > topPos; i--)
                {
                    if (_gMode[i] > _gMode[topPos] * 0.01)
                    {
                        _gModeRight = (ushort) (i * 10);
                        break;
                    }
                }

                _gModeLeftManual = _gModeLeft;
                _gModeRightManual = _gModeRight;
            }
            else
            {
                _gModeLeft = _gModeLeftManual;
                _gModeRight = _gModeRightManual;
            }
        }

        private void LoadPalette(string path)
        {
            var paletteBitmap = new Bitmap(path);

            for (var i = 0; i < Constants.PALETTE_SIZE; i++)
            {
                var color = paletteBitmap.GetPixel(i, 0);
                _palette[i, 0] = color.R;
                _palette[i, 1] = color.G;
                _palette[i, 2] = color.B;
            }

            _framePicture.Palette = _palette;
            _tempGaugePicture.Palette = _palette;
        }

        private void UpdatePictureBoxSizes()
        {
            const int GAUGE_W = 70;

            if (_framePicture.Parent == null || _framePicture.Image == null) return;

            var pw = (double) _framePicture.Parent.Width - GAUGE_W;
            var ph = (double) _framePicture.Parent.Height;
            var iw = (double) _framePicture.Image.Width;
            var ih = (double) _framePicture.Image.Height;

            var scale = Math.Min(pw / iw, ph / ih);

            var w = iw * scale;
            var h = ih * scale;

            _framePicture.Location = new Point((int) ((pw - w) / 2.0),
                                               (int) ((ph - h) / 2.0));
            _framePicture.Size = new Size((int) w, (int) h);

            _tempGaugePicture.Location = new Point(_framePicture.Location.X + _framePicture.Size.Width,
                                                   _framePicture.Location.Y);
            _tempGaugePicture.Size = new Size(GAUGE_W, _framePicture.Size.Height);
        }

        private void UpdateMouseLabelFromFramePicture(Point localPos)
        {
            var coords = _framePicture.LocalToCoords(localPos);
            _mouseLabel.Text = $"({coords.X}, {coords.Y}): {Utils.Thermal.FormatTempString(_tempUnit, _framePicture.DetectTemperature(coords))}";
        }

        private void UpdateMouseLabelFromTempGaugePicture(Point localPos)
        {
            _mouseLabel.Text = Utils.Thermal.FormatTempString(_tempUnit, _tempGaugePicture.DetectTemperature(localPos));
        }

        private void HandleLoad(object sender, EventArgs e)
        {
            var pngFiles = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\Palette").GetFiles("*.png");

            foreach (FileInfo file in pngFiles)
            {
                _paletteCombo.Items.Add(new ComboItem(file.FullName, file.Name.Replace(".png", "")));
            }

            _paletteCombo.SelectedIndex = 1;
        }

        private void HandlePaint(object sender, PaintEventArgs e)
        {
            if (_lastUsableFrame != null && (_liveCheck.Checked || _firstAfterCal))
            {
                _tempGaugePicture.MinTemp = _gModeLeft;
                _tempGaugePicture.MaxTemp = _gModeRight;

                _sliderMinTempLabel.Text = Utils.Thermal.FormatTempString(_tempUnit, _gModeLeft);
                _sliderMaxTempLabel.Text = Utils.Thermal.FormatTempString(_tempUnit, _gModeRight);

                // Set debug labels
                _gModeLeftLabel.Text = _gModeLeft.ToString();
                _gModeRightLabel.Text = _gModeRight.ToString();
                _maxTempRawLabel.Text = _maxTempRaw.ToString();

                // Set sliders position
                if (_autoRange)
                {
                    _minTempSlider.Value = _gModeLeft;
                    _maxTempSlider.Value = _gModeRight;
                }

                lock (_arrID3.SyncRoot)
                    _framePicture.SupplyData(_arrID3, _gModeLeft, _gModeRight);

                _framePicture.UpdateImage();
                _framePicture.Reanalyze();

                if (_recorder != null)
                    _recorder.SupplyFrame(_framePicture.Image as Bitmap);

                if (_firstAfterCal)
                {
                    _firstAfterCal = false;

                    if (_autoSaveCheck.Checked)
                        Output.Screenshot(_framePicture.Image as Bitmap, _outputPathField.Text);
                }

                lock (_gMode.SyncRoot)
                    _histogramPicture.SupplyData(_gMode, _gModeLeft, _gModeRight);

                _histogramPicture.UpdateImage();

                var pictureUnderCursor = _picturePanel.GetChildAtPoint(_picturePanel.PointToClient(MousePosition));

                if (pictureUnderCursor == _framePicture)
                    UpdateMouseLabelFromFramePicture(_framePicture.PointToClient(MousePosition));
                else if (pictureUnderCursor == _tempGaugePicture)
                    UpdateMouseLabelFromTempGaugePicture(_tempGaugePicture.PointToClient(MousePosition));
            }

            UpdatePictureBoxSizes();
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            _stopThread = true;

            if (_thermal != null)
            {
                _thermalThread.Join(500);
                _thermal.Deinit();
            }
        }

        private class ComboItem
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
            var newPal = (ComboItem) _paletteCombo.SelectedItem;
            LoadPalette(newPal.Key);
        }

        private void HandleUnitRadiosCheckedChanged(object sender, EventArgs e)
        {
            if (_unitsKRadio.Checked) _tempUnit = TemperatureUnit.K;
            if (_unitsCRadio.Checked) _tempUnit = TemperatureUnit.C;
            if (_unitsFRadio.Checked) _tempUnit = TemperatureUnit.F;

            _framePicture.TempUnit = _tempUnit;
            _tempGaugePicture.TempUnit = _tempUnit;
        }

        private void HandleManualRangeSwitchButtonClick(object sender, EventArgs e)
        {
            _autoRange = !_autoRange;

            if (_autoRange)
            {
                _manualRangeSwitchButton.Text = "Switch to manual range";
                _dynSlidersCheck.Checked = false;
                _dynSlidersCheck.Visible = false;
            }
            else
            {
                _manualRangeSwitchButton.Text = "Switch to auto range";
                _dynSlidersCheck.Visible = true;
            }
        }

        private void HandleDynSlidersCheckCheckedChanged(object sender, EventArgs e)
        {
            int currentLeftPos = _minTempSlider.Value;
            int currentRightPos = _maxTempSlider.Value;
            int currentDiff = currentRightPos - currentLeftPos;

            if (_dynSlidersCheck.Checked)
            {
                // Left min
                if (currentLeftPos - currentDiff > 4000)
                    _minTempSlider.Minimum = currentLeftPos - currentDiff;
                else
                    _minTempSlider.Minimum = 4000;

                // Left max
                if (currentLeftPos + currentDiff * 2 < 20000)
                    _minTempSlider.Maximum = currentLeftPos + currentDiff * 2;
                else
                    _minTempSlider.Maximum = 20000;

                // Right min
                if (currentRightPos - currentDiff * 2 > 4000)
                    _maxTempSlider.Minimum = currentRightPos - currentDiff * 2;
                else
                    _maxTempSlider.Minimum = 4000;

                // Right max
                if (currentRightPos + currentDiff < 20000)
                    _maxTempSlider.Maximum = currentRightPos + currentDiff;
                else
                    _maxTempSlider.Maximum = 20000;
            }
            else
            {
                _maxTempSlider.Minimum = 4000;
                _minTempSlider.Minimum = 4000;
                _maxTempSlider.Maximum = 20000;
                _minTempSlider.Maximum = 20000;
            }
        }

        private void HandleOutputPathButtonClick(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _outputPathField.Text = dialog.SelectedPath;
                }
            }
        }

        private void HandleScreenshotButtonClick(object sender, EventArgs e)
        {
            if (_framePicture.Image == null) return;
            Output.Screenshot(_framePicture.Image as Bitmap, _outputPathField.Text);
        }

        private void HandleRecordVideoButtonClick(object sender, EventArgs e)
        {
            if (_recorder == null)
            {
                _recorder = new Output.VideoRecorder(_outputPathField.Text);

                if (!_recorder.Start())
                {
                    _recorder = null;
                    return;
                }

                _recordVideoButton.Text = "Stop recording";
            }
            else
            {
                _recorder.Stop();
                _recorder = null;

                _recordVideoButton.Text = "Record video";
            }
        }

        private void HandlePictureMouseEnter(object sender, EventArgs e)
        {
            _mouseLabel.Visible = true;
        }

        private void HandlePictureMouseLeave(object sender, EventArgs e)
        {
            _mouseLabel.Visible = false;
        }

        private void HandleMinTempSliderScroll(object sender, EventArgs e)
        {
            if (_autoRange) return;

            if (_minTempSlider.Value < _maxTempSlider.Value - 10)
            {
                _gModeLeftManual = (ushort) _minTempSlider.Value;
            }
            else
            {
                _minTempSlider.Value = _maxTempSlider.Value - 10;
            }
        }

        private void HandleMaxTempSliderScroll(object sender, EventArgs e)
        {
            if (_autoRange) return;

            if (_maxTempSlider.Value > _minTempSlider.Value + 10)
            {
                _gModeRightManual = (ushort) _maxTempSlider.Value;
            }
            else
            {
                _maxTempSlider.Value = _minTempSlider.Value + 10;
            }
        }
    }
}
