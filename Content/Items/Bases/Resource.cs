using Microsoft.Xna.Framework.Graphics;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Helpers;
using ProjectFrameWar.Core.Items;
using System.Diagnostics;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    struct ResourceData
    {
        public string name;
        public ResourceRarity rarity;
    }

    [Autoload(false)]
    internal class Resource(ResourceData data) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"resource_{data.name}";
        public override string Texture 
            => ModContent.RequestIfExists<Texture2D>($"{ProjectFrameWar.texPath}/resources/{Name}", out var asset) ? 
            $"{Mod.Name}/" + asset.Name : ProjectFrameWar.placeholderPath;

        public override void SetDefaults()
        {
            Item.TryEnableComponent<ResourceComponent>(x => x.rarity = data.rarity);

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