using System.Globalization;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class MiscExtensions
    {
        public static string FormatAsPercent(this float val, int places = 0) 
            => (val * 100).ToString("N", new NumberFormatInfo() { NumberDecimalDigits = places });
    }
}
