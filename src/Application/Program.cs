using System;
using System.Windows.Forms;
using SeekOFix.UI;

namespace SeekOFix
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var mode = args.GetWithFallback(0, "stream");
            var ioPath = args.GetWithFallback(1, "");
            var maxFrames = Int32.Parse(args.GetWithFallback(2, "0"));

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow(mode, ioPath, maxFrames));
        }

        private static string GetWithFallback(this string[] args, int index, string fallback)
        {
            return args.Length > index ? args[index] : fallback;
        }
    }
}
