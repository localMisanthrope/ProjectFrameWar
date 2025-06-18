using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Helpers
{
    internal class JSONHelpers
    {
        public static List<T> CheckObjectList<T>(Mod mod, string path) where T : struct
        {
            if (mod.GetFileBytes(path) is null)
            {
                mod.Logger.Error($"JSON file not found at \"{path}\", or does not exist!");
                return null;
            }

            return JsonConvert.DeserializeObject<List<T>>(Encoding.UTF8.GetString(mod.GetFileBytes(path)));
        }
    }
}
