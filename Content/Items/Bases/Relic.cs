using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Helpers;
using ProjectFrameWar.Core.Items;
using System.Diagnostics;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    struct RelicData
    {
        public string name;
        public RelicEra era;
        public string[] rewards;

        public const int MAX_REWARDS = 6;
    }

    [Autoload(false)]
    internal class Relic(RelicData data) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"relic_{data.era}_{data.name}";

        public override string Texture => ProjectFrameWar.placeholderPath;

        public override void SetDefaults()
        {
            Item.TryEnableComponent<RelicComponent>(x =>
            {
                x.data = data;
                x.refinement = RelicState.Intact;
                x.rewardPercentages = [0.2533f, 0.11f, 0.02f];
            });

            base.SetDefaults();
        }
    }

    internal class RelicLoader : ILoadable
    {
        int count;

        public void Load(Mod mod)
        {
            count = 0;

            var watch = Stopwatch.StartNew();

            var list = JSONHelpers.CheckObjectList<RelicData>(mod, "dat/item_data/RelicData.json");

            if (list is null)
                return;

            foreach (var data in list)
            {
                mod.AddContent(new Relic(data));
                count++;
            }

            watch.Stop();

            mod.Logger.Info($"[FRAMEWAR]: Finished loading Relic(s) ({count}), took {watch.Elapsed.TotalMilliseconds} ms.");
        }

        public void Unload() { }
    }
}
