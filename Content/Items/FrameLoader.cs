using ProjectFrameWar.Core;
using ProjectFrameWar.Core.Items;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    internal class FrameLoader : ILoadable
    {
        public static Dictionary<string, (FrameData data, bool firstEra)> allFrameData;

        public void Load(Mod mod)
        {
            int count = 0;

            allFrameData = new()
            {
                {"Excalibur", (new(270, 270, 100, 1f), true)},
                {"Mag", (new(180, 455, 140, 1f), true)},
                {"Volt", (new(270, 455, 100, 1f), true)}
            };
            
            foreach (var name in allFrameData.Keys)
            {
                var neuroptics = new FramePart(FramePartComponent.PartType.Neuroptics, name);

                mod.AddContent(new FramePart(FramePartComponent.PartType.Chassis, name));
                mod.AddContent(neuroptics);
                mod.AddContent(new FramePart(FramePartComponent.PartType.Systems, name));

                mod.AddContent(new Blueprint(neuroptics.Item, allFrameData[name].firstEra ? $"FirstEra_{FramePartComponent.PartType.Neuroptics}" : ""));

                //mod.AddContent(new WarframeItem_LegsBase(name));
                //mod.AddContent(new WarframeItem_ChestBase(name));
                //mod.AddContent(new WarframeItem_HeadBase(name));

                count++;
            }

            mod.Logger.Info($"[WARFRAME/INFO]: Finished loading items; Warframes ({count}).");
        }

        public void Unload() => allFrameData = null;
    }
}