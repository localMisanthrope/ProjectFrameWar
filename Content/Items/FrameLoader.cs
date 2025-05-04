using ProjectFrameWar.Content.Items.Parts;
using ProjectFrameWar.Core;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    internal class FrameLoader : ILoadable
    {
        public static Dictionary<string, FrameData> allFrameData = new()
        {
            {"Excalibur", new(270, 270, 100, 1f)},
            {"Mag", new(180, 455, 140, 1f)},
            {"Trinity", new() },
            {"Volt", new(270, 455, 100, 1f)}
        };

        public void Load(Mod mod)
        {
            foreach (var name in allFrameData.Keys)
            {
                mod.AddContent(new Chassis(name));
                mod.AddContent(new Neuroptics(name));
                mod.AddContent(new Systems(name));

                //mod.AddContent(new WarframeItem_LegsBase(name));
                //mod.AddContent(new WarframeItem_ChestBase(name));
                //mod.AddContent(new WarframeItem_HeadBase(name));
            }
        }

        public void Unload() { }
    }
}