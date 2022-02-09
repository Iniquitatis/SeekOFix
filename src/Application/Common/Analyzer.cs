using System.Drawing;

namespace SeekOFix.Common
{
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
}
