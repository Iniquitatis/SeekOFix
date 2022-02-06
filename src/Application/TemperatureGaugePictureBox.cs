using System.Drawing;
using System.Windows.Forms;

namespace SeekOFix
{
    public class TemperatureGaugePictureBox : CustomPictureBox
    {
        private string _tempUnit = "K";
        private int _labelCount = 2;
        private ushort _minTemp = 0;
        private ushort _maxTemp = 30000;

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

        private void DrawLabel(Graphics graphics, int current, int maximum)
        {
            const int EDGE_OFFSET = 16;

            var factor = (float) current / (float) maximum;
            var factorScale = (float) (Height - EDGE_OFFSET * 2) / (float) Height;
            var factorOffset = (1.0f - factorScale) / 2.0f;
            var factorFinal = factorOffset + factor * factorScale;

            var x = Width / 2;
            var y = (int) Lerp(0, Height, factorFinal);
            var t = (ushort) Lerp(_maxTemp, _minTemp, factorFinal);

            var text = Utils.FormatTempString(_tempUnit, t);
            graphics.DrawText(new Point(x, y), text, 17.0f, ContentAlignment.MiddleCenter, Color.White, Color.Black);
        }

        private float Lerp(float a, float b, float x)
        {
            return a * (1.0f - x) + b * x;
        }
    }
}
