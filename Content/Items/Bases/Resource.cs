using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    [Autoload(false)]
    internal class Resource(string resourceName, ResourceComponent.ResourceRarity rarity) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Texture => $"{Mod.Name}/res/texture/resources/resource_{resourceName}";

        public override string Name => $"Resource_{resourceName}";

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.material = true;

            Item.TryEnableComponent<ResourceComponent>(x => x.rarity = rarity);

            base.SetDefaults();
        }
    }
}
