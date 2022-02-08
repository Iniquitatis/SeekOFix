namespace SeekOFix
{
    public static class Utils
    {
        public static string FormatTempString(string unit, int rawValue)
        {
            return $"{RawToTemp(unit, rawValue):0.0} {GetTempSymbol(unit)}";
        }

        public static string GetTempSymbol(string unit)
        {
            switch (unit)
            {
                case "K": return "K";
                case "C": return "°C";
                case "F": return "°F";
                default: return "";
            }
        }

        public static double RawToTemp(string unit, int rawValue)
        {
            var tempValue = ((double) rawValue - 5950.0) / 40.0 + 273.15;

            switch (unit)
            {
                case "K": return tempValue;
                case "C": return tempValue - 273.15;
                case "F": return tempValue * 9.0 / 5.0 - 459.67;
                default: return 0.0;
            }
        }
    }
}
