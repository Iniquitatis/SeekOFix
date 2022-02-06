﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SeekOFix
{
    public class AnalyzablePictureBox : CustomPictureBox
    {
        private bool _analysisEnabled = false;
        private string _tempUnit = "K";
        private bool _showTemperature = true;
        private int _crossSize = 16;
        private bool _showExtremes = true;
        private int _maxPoints = 3;
        private ushort[] _rawValues = null;
        private List<Analyzer> _analyzers = new List<Analyzer>();
        private Analyzer _hotAnalyzer = new Analyzer();
        private Analyzer _coldAnalyzer = new Analyzer();

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

        public ushort[] RawValues
        {
            get => _rawValues;
            set { _rawValues = value; Reanalyze(); }
        }

        public AnalyzablePictureBox()
        {
            MouseDown += HandleMouseDown;
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

        public int DetectTemperature(Point coords)
        {
            if (_rawValues == null) return 0;
            return _rawValues[coords.Y * Constants.DATA_W + coords.X];
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
                var text = Utils.FormatTempString(_tempUnit, t);
                g.DrawText(new Point(x, y - hh), text, 18.0f, ContentAlignment.BottomCenter, Color.White, Color.Black);
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
