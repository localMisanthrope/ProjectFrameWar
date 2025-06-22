using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal enum ResourceRarity
    {
        Common,
        Uncommon,
        Rare,
        SpecialRare
    }

    internal class ResourceComponent : ItemComponent
    {
        public ResourceRarity rarity;

        public override void Component_SetDefaults(Item item) => item.maxStack = int.MaxValue;

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new(Mod, "RarityLine", Language.GetTextValue($"{LOCAL_KEY}.ResourceRarity.{rarity}")));
        }
    }
}
