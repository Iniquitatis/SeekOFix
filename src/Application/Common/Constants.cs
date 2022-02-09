namespace SeekOFix.Common
{
    public static class Constants
    {
        public const int DATA_W = 208;
        public const int DATA_H = 156;
        public const int DATA_LENGTH = DATA_W * DATA_H;
        public const int IMAGE_W = DATA_W - 2;
        public const int IMAGE_H = DATA_H;
        public const int IMAGE_UPSCALE_FACTOR = 2;
        public const int FINAL_IMAGE_W = IMAGE_W * IMAGE_UPSCALE_FACTOR;
        public const int FINAL_IMAGE_H = IMAGE_H * IMAGE_UPSCALE_FACTOR;
        public const int HISTOGRAM_SIZE = 2100;
        public const int PALETTE_SIZE = 1001;
    }
}
