using System.Globalization;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class MiscExtensions
    {
        /// <summary>
        /// Formats a given float value as a percentage string.
        /// </summary>
        /// <param name="val"></param>
        /// <param name="places">The amount of decimal places visible in the percentage.</param>
        /// <returns>Reformated number string</returns>
        public static string FormatAsPercent(this float val, int places = 0) 
            => (val * 100).ToString("N", new NumberFormatInfo() { NumberDecimalDigits = places });
    }
}
