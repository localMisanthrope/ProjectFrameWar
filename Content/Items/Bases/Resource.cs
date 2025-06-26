using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Helpers;
using ProjectFrameWar.Core.Items;
using System.Diagnostics;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    struct ResourceData
    {
        public string Name { get; set; }
        public ResourceRarity Rarity { get; set; }
        public bool InfestedMaterial { get; set; }
        public int DecayRate { get; set; }
    }

    [Autoload(false)]
    internal class Resource(ResourceData data) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"resource_{data.Name}";
        public override string Texture => MiscHelpers.CheckTexturePath(Mod, $"{ProjectFrameWar.texPath}/resources/{Name}");

        public override void SetDefaults()
        {
            Item.TryEnableComponent<ResourceComponent>(x => x.rarity = data.Rarity);

            if (data.InfestedMaterial)
                Item.TryEnableComponent<InfectionComponent>();

            if (data.DecayRate > -1)
                Item.TryEnableComponent<DecayComponent>(x => x.decayRate = data.DecayRate);

            base.SetDefaults();
        }
    }

    internal class ResourceLoader : ILoadable
    {
        int count;

        public void Load(Mod mod)
        {
            count = 0;

            var watch = Stopwatch.StartNew();

            foreach (var data in JSONHelpers.CheckObjectList<ResourceData>(mod, "dat/item_data/ResourceData.json"))
            {
                mod.AddContent(new Resource(data));
                count++;
            }

            watch.Stop();

            mod.Logger.Info($"[FRAMEWAR]: Finished loading Item(s) ({count}), took {watch.Elapsed.TotalMilliseconds} ms.");
        }

        public void Unload() { }
    }
}