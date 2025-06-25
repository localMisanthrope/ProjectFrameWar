using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Helpers;
using ProjectFrameWar.Core.Items;
using System;
using System.Diagnostics;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    struct BlueprintData
    {
        public string[] Ingredients { get; set; }
        public int[] Requirements { get; set; }
        public string Result { get; set; }
        public int ResultAmount { get; set; }
        public int CraftTime { get; set; }
    }

    [Autoload(false)]
    internal class Blueprint(BlueprintData data, BlueprintCategory category) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"blueprint_{data.Result}";
        public override string Texture => $"{ProjectFrameWar.texPath}/blueprint_Back";

        public override void SetDefaults()
        {
            Item.TryEnableComponent<BlueprintComponent>(x =>
            {
                x.data = data;
                x.counts = new int[data.Ingredients.Length];
                x.category = category;
                x.craftTimer = 0;
            });

            base.SetDefaults();
        }
    }

    internal class BlueprintLoader : ILoadable
    {
        int count;

        int subCount;

        public void Load(Mod mod)
        {
            count = 0;

            var watch = Stopwatch.StartNew();
            var subWatch = Stopwatch.StartNew();

            foreach (var value in Enum.GetValues(typeof(BlueprintCategory))) 
            {
                subCount = 0;

                subWatch.Restart();

                var list = JSONHelpers.CheckObjectList<BlueprintData>(mod, $"dat/blueprint_data/{value}BlueprintData.json");

                if (list is null)
                {
                    mod.Logger.Warn($"[FRAMEWAR]: Failed to load \"{value}BlueprintData.json\". Skipping...");
                    continue;
                }   

                foreach (var data in list)
                {
                    mod.AddContent(new Blueprint(data, (BlueprintCategory)value));
                    count++;
                    subCount++;
                }

                subWatch.Stop();

                mod.Logger.Info($"[FRAMEWAR]: Finished loading {value} Blueprints ({subCount}), took {subWatch.Elapsed.TotalMilliseconds} ms.");
            }

            watch.Stop();

            mod.Logger.Info($"[FRAMEWAR]: Finished loading Blueprints ({count}), took {watch.Elapsed.TotalMilliseconds} ms.");
        }

        public void Unload() { }
    }
}