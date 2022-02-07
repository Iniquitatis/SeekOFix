using System;
using System.Drawing;
using System.IO;
using AForge.Video.FFMPEG;

namespace SeekOFix
{
    public static class Output
    {
        public static void Screenshot(Bitmap image, string path)
        {
            if (image == null || !Directory.Exists(path)) return;

            image.Save(path + FormatFileName("png"));
        }

        private static string FormatFileName(string extension)
        {
            return @"\seek_" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss_fff") + $".{extension}";
        }

        public class VideoRecorder
        {
            private string _path;
            private VideoFileWriter _writer;
            private DateTime _startDate;

            public VideoRecorder(string path)
            {
                _path = path;
            }

            public bool Start()
            {
                if (!Directory.Exists(_path)) return false;

                _writer = new VideoFileWriter();
                _writer.Open(_path + FormatFileName("avi"), Constants.IMAGE_W * 2, Constants.IMAGE_H * 2, 24, VideoCodec.MPEG4);

                _startDate = DateTime.Now;

                return true;
            }

            public void Stop()
            {
                _writer.Close();
            }

            public void SupplyFrame(Bitmap image)
            {
                if (image == null) return;

                _writer.WriteVideoFrame(image, DateTime.Now - _startDate);
            }
        }
    }
}
