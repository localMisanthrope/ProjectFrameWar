using System.Collections.Generic;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria;

namespace ProjectFrameWar.Core.Items
{
    internal class DecayComponent : ItemComponent
    {
        public int decayRate;

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new(Mod, "DecayLine", "\n" + Language.GetText($"{LOCAL_KEY}.DecayDescription").Value));
        }
    }
}