using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items.WeaponComponents
{
    internal class RivenComponent : ItemComponent
    {
        public bool unlocked;
        public RivenState state;

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            var supplementText = unlocked ? $"{state} " : "Encased ";
            tooltips[0].Text = supplementText + item.Name;
            tooltips[0].OverrideColor = state switch
            {
                RivenState.Encased => Color.Gray,
                RivenState.Sundered => Color.Purple,
                RivenState.Phased => Color.AliceBlue,
                RivenState.Unstable => Color.Orange,
                _ => Color.Gray
            };

            if (!unlocked)
                tooltips.Add(new(Mod, "EncasedRivenLine", "This weapon is encased in Void Crystal.\nComplete the challenge to unleash its power."));

            base.Component_ModifyTooltips(item, tooltips);
        }

        public void OnUnlocked()
        {
            state = (RivenState)Main.rand.Next(1, 4);
        }

        public enum RivenState
        {
            Encased,
            Sundered,
            Phased,
            Unstable
        }
    }
}
