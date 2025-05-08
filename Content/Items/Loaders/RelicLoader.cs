using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

using static ProjectFrameWar.Core.Items.RelicComponent;

namespace ProjectFrameWar.Content.Items.Loaders
{
    internal class RelicLoader : ILoadable
    {
        /// <summary>
        /// A dictionary of relic rewards, indexed by the "series" of the Relic (For example, E1).
        /// </summary>
        public Dictionary<string, (RelicEra era, Item[] relicRewards)> relicData;

        public void Load(Mod mod)
        {
            relicData = new()
            {
                {"E1", (RelicEra.Lith, [
                    ItemExtensions.GetItem<Resource>("Resource_AlloyPlate"),
                    ItemExtensions.GetItem<Resource>("Resource_Rubedo"),
                    ItemExtensions.GetItem<Resource>("Resource_Plastids"),
                    ItemExtensions.GetItem<Resource>("Resource_PolymerBundle"),
                    ItemExtensions.GetItem<Resource>("Resource_ControlModule"),
                    ItemExtensions.GetItem<FramePart>("Neuroptics_Excalibur")])},
            };

            int count = 0;

            foreach (var relic in  relicData.Keys) 
            {
                mod.AddContent(new Relic(relicData[relic].era, relic, relicData[relic].relicRewards));
                count++;
            }

            mod.Logger.Info($"[WARFRAME]: Finished loading Items; Relics. ({count})");
        }

        public void Unload() { }
    }
}