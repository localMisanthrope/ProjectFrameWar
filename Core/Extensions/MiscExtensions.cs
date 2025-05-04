using System.Linq;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class MiscExtensions
    {
        public static string RemoveWhite(this string input) => new(input.Where(x => !char.IsWhiteSpace(x)).ToArray());
    }
}