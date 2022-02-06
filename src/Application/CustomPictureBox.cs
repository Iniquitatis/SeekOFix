using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SeekOFix
{
    public class CustomPictureBox : PictureBox
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Image == null) return;

            var attrs = new ImageAttributes();
            attrs.SetWrapMode(WrapMode.TileFlipXY);

            var g = e.Graphics;
            g.InterpolationMode = InterpolationMode.Bilinear;
            g.DrawImage(Image, new Rectangle(Point.Empty, Size), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, attrs);
            g.InterpolationMode = InterpolationMode.Default;
        }
    }
}
