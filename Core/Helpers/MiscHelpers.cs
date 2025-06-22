using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Helpers
{
    internal class MiscHelpers
    {
        public static string CheckTexturePath(Mod mod, string path)
            => ModContent.RequestIfExists<Texture2D>(path, out var asset) ? $"{mod.Name}/" + asset.Name : ProjectFrameWar.placeholderPath;
    }
}
