using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal enum PartType
    {
        Chassis,
        Neuroptics,
        Systems
    }

    internal class FramePartComponent : ItemComponent
    {
        internal FrameData data;

        internal PartType type;

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Insert(1, new(Mod, "FramePartTooltip", Language.GetText($"{LOCAL_KEY}.FramePartDescription").Format(type, data.Name)));
        }
    }
}