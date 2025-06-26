using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal class InfectionComponent : ItemComponent
    {
        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new(Mod, "InfectionLine", "\n" + Language.GetText($"{LOCAL_KEY}.InfectionDescription").Value));
        }
    }
}