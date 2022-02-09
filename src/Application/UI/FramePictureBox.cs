using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging.Filters;
using SeekOFix.Common;

using static SeekOFix.Utils.Drawing;

namespace SeekOFix.UI
{
    public class FramePictureBox : CustomPictureBox
    {
        private byte[,] _palette = new byte[Constants.PALETTE_SIZE, 3];
        private bool _applyDenoising = true;
        private bool _applySharpening = false;
        private bool _analysisEnabled = false;
        private string _tempUnit = "K";
        private bool _showTemperature = true;
        private int _crossSize = 16;
        private bool _showExtremes = true;
        private int _maxPoints = 3;
        private ushort[] _data = new ushort[Constants.DATA_LENGTH];
        private ushort _gModeLeft = 0;
        private ushort _gModeRight = 0;
        private Bitmap _frameBitmap = new Bitmap(Constants.DATA_W, Constants.DATA_H, PixelFormat.Format24bppRgb);
        private Bitmap _finalBitmap = null;
        private List<Analyzer> _analyzers = new List<Analyzer>();
        private Analyzer _hotAnalyzer = new Analyzer();
        private Analyzer _coldAnalyzer = new Analyzer();

        public byte[,] Palette
        {
            get => _palette;
            set { _palette = value; UpdateImage(); Invalidate(); }
        }

        public bool ApplyDenoising
        {
            get => _applyDenoising;
            set { _applyDenoising = value; UpdateImage(); Invalidate(); }
        }

        public bool ApplySharpening
        {
            get => _applySharpening;
            set { _applySharpening = value; UpdateImage(); Invalidate(); }
        }

        public bool AnalysisEnabled
        {
            get => _analysisEnabled;
            set { _analysisEnabled = value; Reanalyze(); }
        }

        public string TempUnit
        {
            get => _tempUnit;
            set { _tempUnit = value; Invalidate(); }
        }

        public bool ShowTemperature
        {
            get => _showTemperature;
            set { _showTemperature = value; Invalidate(); }
        }

        public int CrossSize
        {
            get => _crossSize;
            set { _crossSize = value; Invalidate(); }
        }

        public bool ShowExtremes
        {
            get => _showExtremes;
            set { _showExtremes = value; Reanalyze(); }
        }

        public int MaxPoints
        {
            get => _maxPoints;
            set { _maxPoints = value; AdjustAnalyzerCount(); Invalidate(); }
        }

        public FramePictureBox()
        {
            MouseDown += HandleMouseDown;
        }

        public void SupplyData(ushort[] data, ushort gModeLeft, ushort gModeRight)
        {
            Buffer.BlockCopy(data, 0, _data, 0, Constants.DATA_LENGTH * sizeof(ushort));
            _gModeLeft = gModeLeft;
            _gModeRight = gModeRight;
        }

        public void UpdateImage()
        {
            unsafe
            {
                var data = _frameBitmap.LockBits(new Rectangle(0, 0, _frameBitmap.Width, _frameBitmap.Height), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);
                var bytes = (byte*) data.Scan0;

                var scale = (double) (_gModeRight - _gModeLeft) / 1000.0;

                Parallel.For(0, Constants.DATA_LENGTH, i =>
                {
                    var value = Utils.Math.Clamp(_data[i], _gModeLeft, _gModeRight);
                    value -= _gModeLeft;

                    var index = (ushort) ((double) value / scale);

                    bytes[i * 3 + 0] = _palette[index, 2];
                    bytes[i * 3 + 1] = _palette[index, 1];
                    bytes[i * 3 + 2] = _palette[index, 0];
                });

                _frameBitmap.UnlockBits(data);
            }

            var cropFilter = new Crop(new Rectangle(0, 0, Constants.IMAGE_W, Constants.IMAGE_H));
            var croppedBitmap = cropFilter.Apply(_frameBitmap);

            if (_applyDenoising)
            {
                var denoisingFilter = new BilateralSmoothing();
                denoisingFilter.ApplyInPlace(croppedBitmap);
            }

            var upscaleFilter = new ResizeNearestNeighbor(Constants.FINAL_IMAGE_W, Constants.FINAL_IMAGE_H);
            _finalBitmap = upscaleFilter.Apply(croppedBitmap);

            if (_applySharpening)
            {
                var sharpenFilter = new GaussianSharpen(1.0f);
                sharpenFilter.ApplyInPlace(_finalBitmap);
            }

            Image = _finalBitmap;
        }

        public void Reanalyze()
        {
            DetectExtremes();

            foreach (var analyzer in _analyzers)
            {
                analyzer.temperature = DetectTemperature(analyzer.coords);
            }

            Invalidate();
        }

        public void DeleteAllAnalyzers()
        {
            _analyzers.Clear();
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

        public ushort DetectTemperature(Point coords)
        {
            return _data[coords.Y * Constants.DATA_W + coords.X];
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (!_analysisEnabled) return;

            var g = e.Graphics;
            g.EnableHQGraphics();

            foreach (var analyzer in _analyzers)
            {
                DrawAnalyzer(g, analyzer, Color.White);
            }

            if (_showExtremes)
            {
                DrawAnalyzer(g, _hotAnalyzer, Color.Orange);
                DrawAnalyzer(g, _coldAnalyzer, Color.LightBlue);
            }

            g.DisableHQGraphics();
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

        private void DrawAnalyzer(Graphics graphics, Analyzer analyzer, Color color)
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
                var text = Utils.Thermal.FormatTempString(_tempUnit, t);
                g.DrawText(new Point(x, y - hh), text, 18.0f, ContentAlignment.BottomCenter, Color.White, Color.Black);
            }

            crossPath.Dispose();
            lineFill.Dispose();
            lineOutline.Dispose();
        }

        private void DetectExtremes()
        {
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
                    var value = _data[y * Constants.DATA_W + x];

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

            _coldAnalyzer.coords.X = lx;
            _coldAnalyzer.coords.Y = ly;
            _coldAnalyzer.temperature = lowest;

            _hotAnalyzer.coords.X = hx;
            _hotAnalyzer.coords.Y = hy;
            _hotAnalyzer.temperature = highest;
        }

        private void AdjustAnalyzerCount()
        {
            while (_analyzers.Count > _maxPoints)
                _analyzers.RemoveAt(0);
        }
    }
}
