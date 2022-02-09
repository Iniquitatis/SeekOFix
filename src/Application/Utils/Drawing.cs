using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace SeekOFix.Utils
{
    public static class Drawing
    {
        public static void EnableHQGraphics(this Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
        }

        public static void DisableHQGraphics(this Graphics graphics)
        {
            graphics.CompositingQuality = CompositingQuality.Default;
            graphics.InterpolationMode = InterpolationMode.Default;
            graphics.SmoothingMode = SmoothingMode.None;
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
        }

        public static void DrawText(this Graphics graphics,
                                    Point origin,
                                    string text,
                                    float size,
                                    ContentAlignment alignment,
                                    Color fillColor,
                                    Color outlineColor)
        {
            var outline = new Pen(outlineColor, 4.0f);
            outline.LineJoin = LineJoin.Round;

            var fill = new SolidBrush(fillColor);

            var font = new FontFamily("Segoe UI");

            var format = new StringFormat();
            format.Alignment = alignment.ToStringAlignmentX();
            format.LineAlignment = alignment.ToStringAlignmentY();

            var path = new GraphicsPath();
            path.AddString(text, font, (int) FontStyle.Regular, size, origin, format);

            graphics.DrawPath(outline, path);
            graphics.FillPath(fill, path);

            path.Dispose();
            format.Dispose();
            font.Dispose();
            fill.Dispose();
            outline.Dispose();
        }

        private static StringAlignment ToStringAlignmentX(this ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.MiddleLeft:
                case ContentAlignment.BottomLeft:
                    return StringAlignment.Near;

                case ContentAlignment.TopCenter:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.BottomCenter:
                    return StringAlignment.Center;

                case ContentAlignment.TopRight:
                case ContentAlignment.MiddleRight:
                case ContentAlignment.BottomRight:
                    return StringAlignment.Far;

                default:
                    return new StringAlignment();
            }
        }

        private static StringAlignment ToStringAlignmentY(this ContentAlignment alignment)
        {
            switch (alignment)
            {
                case ContentAlignment.TopLeft:
                case ContentAlignment.TopCenter:
                case ContentAlignment.TopRight:
                    return StringAlignment.Near;

                case ContentAlignment.MiddleLeft:
                case ContentAlignment.MiddleCenter:
                case ContentAlignment.MiddleRight:
                    return StringAlignment.Center;

                case ContentAlignment.BottomLeft:
                case ContentAlignment.BottomCenter:
                case ContentAlignment.BottomRight:
                    return StringAlignment.Far;

                default:
                    return new StringAlignment();
            }
        }
    }
}
