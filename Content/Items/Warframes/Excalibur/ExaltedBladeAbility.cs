using ProjectFrameWar.Core;
using Terraria;

namespace ProjectFrameWar.Content.Items.Warframes.Excalibur
{
    //To-do: Sound playing.

    internal class ExaltedBladeAbility : Ability
    {
        public Item exaltedBladeItem;
        public int heldItemIndex = 0;

        internal int drainTimer = 0;

        public override void SetDefaults()
        {


            energyCost = 25;
            base.SetDefaults();
        }

        public override void OnCast(Player player)
        {
            heldItemIndex = player.selectedItem;
            EnergyComp(player).SpendEnergy(energyCost);
            drainTimer = 60;
        }

        public override void OnRecast(Player player)
        {
            player.selectedItem = heldItemIndex;

            base.OnRecast(player);
        }

        public override void UpdateEffect(Player player)
        {
            if (player == Main.LocalPlayer)
            {
                player.selectedItem = 58;
                Main.mouseItem = exaltedBladeItem;
            }

            var drainCost = energyCost / 5;
            drainTimer--;

            if (drainTimer <= 0)
            {
                EnergyComp(player).SpendEnergy(drainCost);
                drainTimer = 60;
            }

            base.UpdateEffect(player);
        }
    }
}