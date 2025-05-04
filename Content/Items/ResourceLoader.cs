using System.Collections.Generic;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    internal class ResourceLoader : ILoadable
    {
        public List<KeyValuePair<string, int>> resourceNames;

        public void Load(Mod mod)
        {
            resourceNames = [
                new("AlloyPlate", 0), 
                new("Circuits", 1),
                new("Ferrite", 0), 
                new("NeuralSensor", 2), 
                new("Rubedo", 1), 
                new("VoidTrace", 1)];

            foreach (var name in resourceNames)
                mod.AddContent(new Resource(name.Key, name.Value));
        }

        public void Unload() { }
    }
}
