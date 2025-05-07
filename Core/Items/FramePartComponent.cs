using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal class FramePartComponent : ItemComponent
    {
        public bool isPrime;

        public bool isUmbra;

        public PartType type;

        public string frameName;

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            frameName += isPrime ? " Prime" : string.Empty;
            frameName += isUmbra ? " Umbra" : string.Empty;

            tooltips[0].Text.Insert(0, frameName + " ");

            tooltips.Insert(1, new(Mod, "FramePartTooltip",
                Language.GetText("Mods.ProjectFrameWar.Warframes.Frame_Part_Format").Format(type, frameName)));

            base.Component_ModifyTooltips(item, tooltips);
        }

        public enum PartType
        {
            Chassis,
            Neuroptics,
            Systems
        }
    }
}