using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Helpers;
using ProjectFrameWar.Core.Items;
using System.Diagnostics;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core
{
    struct FrameData
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Shield { get; set; }
        public int Armor { get; set; }
        public int Energy { get; set; }
        public float Sprint { get; set; }

        public bool Prime { get; set; }
    }

    internal class FrameLoader : ILoadable
    {
        int count;

        public void Load(Mod mod)
        {
            count = 0;

            var watch = Stopwatch.StartNew();

            foreach (var data in JSONHelpers.CheckObjectList<FrameData>(mod, "dat/FrameData.json"))
            {
                mod.AddContent(new FramePart(data, PartType.Chassis));
                mod.AddContent(new FramePart(data, PartType.Neuroptics));
                mod.AddContent(new FramePart(data, PartType.Systems));

                count++;
            }

            watch.Stop();

            mod.Logger.Info($"[FRAMEWAR]: Finished loading Warframes ({count}), took {watch.Elapsed.TotalMilliseconds} ms.");
        }

        public void Unload() { }
    }
}