namespace SeekOFix
{
    public static class MathUtils
    {
        public static ushort Clamp(ushort value, ushort minimum, ushort maximum)
        {
            return value < minimum ? minimum : value > maximum ? maximum : value;
        }

        public static float Lerp(float a, float b, float x)
        {
            return a * (1.0f - x) + b * x;
        }
    }
}
