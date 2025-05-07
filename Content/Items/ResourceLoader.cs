using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using System.Collections.Generic;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    internal class ResourceLoader : ILoadable
    {
        public List<KeyValuePair<string, ResourceComponent.ResourceRarity>> resourceNames;

        public void Load(Mod mod)
        {
            resourceNames = [
                new("AlloyPlate", ResourceComponent.ResourceRarity.Common), 
                new("Circuits", ResourceComponent.ResourceRarity.Uncommon),
                new("ControlModule", ResourceComponent.ResourceRarity.Rare),
                new("Ferrite", ResourceComponent.ResourceRarity.Common), 
                new("NeuralSensor", ResourceComponent.ResourceRarity.Rare), 
                new("OrokinCell", ResourceComponent.ResourceRarity.Rare),
                new("Plastids", ResourceComponent.ResourceRarity.Uncommon),
                new("PolymerBundle", ResourceComponent.ResourceRarity.Uncommon),
                new("Rubedo", ResourceComponent.ResourceRarity.Uncommon), 
                new("VoidTrace", ResourceComponent.ResourceRarity.Uncommon)];

            foreach (var name in resourceNames)
                mod.AddContent(new Resource(name.Key, name.Value));

            mod.AddContent(new Blueprint(ItemExtensions.GetItem<Resource>("Resource_NeuralSensor")));

            mod.Logger.Info("[WARFRAME/INFO]: Finished loading items; Resources.");
        }

        public void Unload() => resourceNames = null;
    }
}
