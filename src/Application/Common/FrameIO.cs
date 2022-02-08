using System;
using System.IO;
using winusbdotnet.UsbDevices;

namespace SeekOFix
{
    public static class FrameIO
    {
        public class Writer
        {
            private int _maxFrames;
            private FileStream _stream;
            private int _framesCaptured = 0;

            public Writer(string path, int maxFrames = 100)
            {
                _maxFrames = maxFrames;
                _stream = new FileStream(path, FileMode.Create);
            }

            public void Write(ThermalFrame frame)
            {
                if (_framesCaptured < _maxFrames)
                {
                    Console.WriteLine($"Capturing frame {_framesCaptured}");

                    _stream.Write(frame.RawData, 0, frame.RawData.Length);
                    _framesCaptured++;
                }
                else if (_stream != null)
                {
                    _stream.Flush();
                    _stream.Close();
                    _stream = null;
                }
            }
        }

        public class Reader
        {
            private FileStream _stream;
            private byte[] _data = new byte[Constants.DATA_LENGTH * 2];

            public Reader(string path)
            {
                _stream = new FileStream(path, FileMode.Open);
            }

            public ThermalFrame Read()
            {
                _stream.Read(_data, 0, _data.Length);

                if (_stream.Position == _stream.Length)
                    _stream.Position = 0;

                return new ThermalFrame(_data);
            }
        }
    }
}
