﻿using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace SeekOFix
{
    public class HistogramPictureBox : CustomPictureBox
    {
        const int WIDTH = 200;
        const int HEIGHT = 100;
        const int DRAW_WIDTH = WIDTH - 1;
        const int DRAW_HEIGHT = HEIGHT - 1;

        private ushort[] _gMode = new ushort[Constants.HISTOGRAM_SIZE];
        private ushort _gModeLeft = 0;
        private ushort _gModeRight = 0;
        private Bitmap _bitmap = new Bitmap(WIDTH, HEIGHT, PixelFormat.Format24bppRgb);

        public void SupplyData(ushort[] gMode, ushort gModeLeft, ushort gModeRight)
        {
            Buffer.BlockCopy(gMode, 0, _gMode, 0, Constants.HISTOGRAM_SIZE * 2);
            _gModeLeft = gModeLeft;
            _gModeRight = gModeRight;
        }

        public void UpdateImage()
        {
            var valueCount = (_gModeRight - _gModeLeft) / 10;
            var leftBorder = _gModeLeft / 10;

            using (var g = Graphics.FromImage(_bitmap))
            {
                g.EnableHQGraphics();
                g.Clear(Color.White);

                if (valueCount > 1)
                {
                    var curveFill = new SolidBrush(Color.LightGray);
                    var curveLine = new Pen(Color.Black, 2.0f);

                    var points = new Point[valueCount + 2];
                    points[0].X = 0;
                    points[0].Y = DRAW_HEIGHT;

                    for (var i = 0; i < valueCount; i++)
                    {
                        var pointIndex = i + 1;
                        var factor = (float) i / (float) (valueCount - 1);
                        points[pointIndex].X = (int) ((float) DRAW_WIDTH * factor);
                        points[pointIndex].Y = DRAW_HEIGHT - _gMode[leftBorder + i];
                    }

                    points[points.Length - 1].X = DRAW_WIDTH;
                    points[points.Length - 1].Y = DRAW_HEIGHT;

                    g.FillClosedCurve(curveFill, points);
                    g.DrawClosedCurve(curveLine, points);

                    curveLine.Dispose();
                    curveFill.Dispose();
                }

                g.DisableHQGraphics();
            }

            Image = _bitmap;
        }
    }
}