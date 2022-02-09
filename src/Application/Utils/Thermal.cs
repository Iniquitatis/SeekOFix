using SeekOFix.Common;

namespace SeekOFix.Utils
{
    public static class Thermal
    {
        public static string FormatTempString(TemperatureUnit unit, int rawValue)
        {
            return $"{RawToTemp(unit, rawValue):0.0} {GetTempSymbol(unit)}";
        }

        public static string GetTempSymbol(TemperatureUnit unit)
        {
            switch (unit)
            {
                case TemperatureUnit.K: return "K";
                case TemperatureUnit.C: return "°C";
                case TemperatureUnit.F: return "°F";
                default: return "";
            }
        }

        public static double RawToTemp(TemperatureUnit unit, int rawValue)
        {
            var tempValue = ((double) rawValue - 5950.0) / 40.0 + 273.15;

            switch (unit)
            {
                case TemperatureUnit.K: return tempValue;
                case TemperatureUnit.C: return tempValue - 273.15;
                case TemperatureUnit.F: return tempValue * 9.0 / 5.0 - 459.67;
                default: return 0.0;
            }
        }
    }
}
