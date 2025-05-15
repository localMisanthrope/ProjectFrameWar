using ProjectFrameWar.Core;
using ProjectFrameWar.Core.Extensions;
using Terraria;

namespace ProjectFrameWar.Content.Items.Warframes.Excalibur
{
    internal class RadialBlindAbility : Ability
    {
        public int blindDuration = 3500; //15 seconds.

        public float blindRadius = 25f; //25m.

        public override void SetDefaults()
        {
            energyCost = 50;

            base.SetDefaults();
        }

        public override void OnCast(Player player)
        {
            EnergyComp(player).SpendEnergy(energyCost);

            foreach (var npc in player.SurroundingNPCs(blindRadius))
            {
                //To-do:
                //  Add debuff to NPCs for blind.
                //  Make duration last for blindDuration in ticks.
            }

            base.OnCast(player);
        }
    }
}