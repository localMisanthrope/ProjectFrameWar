using ProjectFrameWar.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal class TestWeaponComponent : ItemComponent
    {
        public override void OnEnabled(Item item)
        {
            item.ModItem.Mod.Logger.Info($"[FRAMEWAR]: If enabled for {item.Name}, I should show up!");

            base.OnEnabled(item);
        }

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new(Mod, "TestLine", "This is a test custom weapon. \n If this component worked, you should be seeing this."));
        }
    }
}