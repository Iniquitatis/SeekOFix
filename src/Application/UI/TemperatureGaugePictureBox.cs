using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SeekOFix
{
    public class TemperatureGaugePictureBox : CustomPictureBox
    {
        private const int LABEL_EDGE_OFFSET = 16;

        private byte[,] _palette = new byte[Constants.PALETTE_SIZE, 3];
        private string _tempUnit = "K";
        private int _labelCount = 2;
        private ushort _minTemp = 0;
        private ushort _maxTemp = 30000;
        private Bitmap _bitmap = new Bitmap(1, Constants.PALETTE_SIZE, PixelFormat.Format24bppRgb);

        public byte[,] Palette
        {
            get => _palette;
            set { _palette = value; UpdateImage(); Invalidate(); }
        }

        public string TempUnit
        {
            get => _tempUnit;
            set { _tempUnit = value; Invalidate(); }
        }

        public int LabelCount
        {
            get => _labelCount;
            set { _labelCount = value; Invalidate(); }
        }

        public ushort MinTemp
        {
            get => _minTemp;
            set { _minTemp = value; Invalidate(); }
        }

        public ushort MaxTemp
        {
            get => _maxTemp;
            set { _maxTemp = value; Invalidate(); }
        }

        public ushort DetectTemperature(Point localPos)
        {
            return (ushort) MathUtils.Lerp(_minTemp, _maxTemp, (float) (Height - localPos.Y) / (float) Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            g.EnableHQGraphics();

            for (var i = 0; i < _labelCount; i++)
            {
                DrawLabel(e.Graphics, i, _labelCount - 1);
            }

            g.DisableHQGraphics();
        }

        private void UpdateImage()
        {
            for (var i = 0; i < Constants.PALETTE_SIZE; i++)
            {
                _bitmap.SetPixel(0, Constants.PALETTE_SIZE - 1 - i, Color.FromArgb(_palette[i, 0],
                                                                                   _palette[i, 1],
                                                                                   _palette[i, 2]));
            }

            Image = _bitmap;
        }

        private void DrawLabel(Graphics graphics, int current, int maximum)
        {
            var factor = (float) current / (float) maximum;
            var factorScale = (float) (Height - LABEL_EDGE_OFFSET * 2) / (float) Height;
            var factorOffset = (1.0f - factorScale) / 2.0f;
            var factorFinal = factorOffset + factor * factorScale;

            var x = Width / 2;
            var y = (int) MathUtils.Lerp(0, Height, factorFinal);
            var t = (ushort) MathUtils.Lerp(_maxTemp, _minTemp, factorFinal);

            var text = Utils.FormatTempString(_tempUnit, t);
            graphics.DrawText(new Point(x, y), text, 17.0f, ContentAlignment.MiddleCenter, Color.White, Color.Black);
        }
    }
}
