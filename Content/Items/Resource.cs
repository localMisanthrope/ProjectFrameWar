using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    [Autoload(false)]
    internal class Resource(string resourceName, int rarity) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Texture => $"ProjectFrameWar/res/texture/resources/resource_{resourceName}";
        public override string Name => $"Resource_{resourceName}";

        public readonly string resourceName = resourceName;

        public readonly int rarity = rarity;

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.material = true;

            Item.TryEnableComponent<ResourceComponent>(x => x.rarity = (ResourceComponent.ResourceRarity)rarity);

            base.SetDefaults();
        }
    }
}
