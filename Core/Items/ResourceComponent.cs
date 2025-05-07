using Terraria;

namespace ProjectFrameWar.Core.Items
{
    internal class ResourceComponent : ItemComponent
    {
        public ResourceRarity rarity;

        public override void Component_SetDefaults(Item item)
        {
            item.maxStack = 99;

            base.Component_SetDefaults(item);
        }

        public enum ResourceRarity
        {
            Common,
            Uncommon,
            Rare
        }
    }
}