using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Loaders
{
    internal class ResourceLoader : ILoadable
    {
        public Dictionary<string, ResourceComponent.ResourceRarity> resourceNames;

        public void Load(Mod mod)
        {
            int count = 0;

            resourceNames = new Dictionary<string, ResourceComponent.ResourceRarity>
            {
                { "AlloyPlate", ResourceComponent.ResourceRarity.Common },
                { "Circuits", ResourceComponent.ResourceRarity.Uncommon },
                { "ControlModule", ResourceComponent.ResourceRarity.Rare },
                { "Ferrite", ResourceComponent.ResourceRarity.Common },
                { "Morphics", ResourceComponent.ResourceRarity.Rare },
                { "NeuralSensor", ResourceComponent.ResourceRarity.Rare },
                { "OrokinCell", ResourceComponent.ResourceRarity.Rare },
                { "Plastids", ResourceComponent.ResourceRarity.Uncommon },
                { "PolymerBundle", ResourceComponent.ResourceRarity.Uncommon },
                { "Rubedo", ResourceComponent.ResourceRarity.Uncommon },
                { "Salvage", ResourceComponent.ResourceRarity.Common },
                { "VoidTrace", ResourceComponent.ResourceRarity.Uncommon }
            };

            foreach (var name in resourceNames)
            {
                mod.AddContent(new Resource(name.Key, name.Value));
                count++;
            }  

            mod.AddContent(new Blueprint(ItemExtensions.GetItem<Resource>("Resource_NeuralSensor")));

            mod.Logger.Info($"[WARFRAME/INFO]: Finished loading items; Resources. ({count})");
        }

        public void Unload() => resourceNames = null;
    }
}
