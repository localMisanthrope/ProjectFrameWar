using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core;
using ProjectFrameWar.Core.Items;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Loaders
{
    internal class FrameLoader : ILoadable
    {
        public static Dictionary<string, FrameData> allFrameData;

        public void Load(Mod mod)
        {
            int count = 0;

            allFrameData = new()
            {
                {"Excalibur", new(270, 270, 100, 1, 1f)},
                {"Frost", new(270, 270, 100, 315, 0.95f)},
                {"Mag", new(180, 455, 140, 105, 1f)},
                {"Volt", new(270, 455, 100, 105, 1f)}
            };

            foreach (var name in allFrameData.Keys)
            {
                var chassis = new FramePart(FramePartComponent.PartType.Chassis, name);
                var neuroptics = new FramePart(FramePartComponent.PartType.Neuroptics, name);
                var systems = new FramePart(FramePartComponent.PartType.Systems, name);

                mod.AddContent(chassis);
                mod.AddContent(neuroptics);
                mod.AddContent(systems);

                //mod.AddContent(new WarframeItem_LegsBase(name));
                //mod.AddContent(new WarframeItem_ChestBase(name));
                //mod.AddContent(new WarframeItem_HeadBase(name));

                count++;
            }

            mod.Logger.Info($"[WARFRAME/INFO]: Finished loading items; Warframes ({count}).");
        }

        public void Unload() => allFrameData = null;

        //{"Ash", (new(455, 270, 100, 105, 1.15f), true) },
        //{"Ember", (new(270, 270, 175, 135, 1.1f), true) },
        //{"Loki", (new(180, 180, 175, 105, 1.25f), true) },
        //{"Nyx", (new(270, 270, 175, 105, 1.1f), true) },
        //{"Trinity", (new (270, 270, 175, 105, 1f), true) },
    }
}