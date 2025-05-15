using Microsoft.Xna.Framework;
using ProjectFrameWar.Core.Extensions;
using System;
using Terraria;

namespace ProjectFrameWar.Core.Players
{
    internal class EnergyComponent : PlayerComponent
    {
        public int energyCurrent;

        public int energyMax;

        public float energyRegenRate;

        public void HealEnergy(int amount)
        {
            energyCurrent = Math.Clamp(energyCurrent + amount, 0, energyMax);
            CombatText.NewText(Player.getRect(), Color.CadetBlue, amount, amount > 50);
        }

        public void SpendEnergy(int amount) => energyCurrent = Math.Clamp(energyCurrent - amount, 0, energyMax);

        public override void Component_PreUpdate()
        {
            if (!Player.TryGetComponent(out WarframeComponent wFC))
                return;

            energyMax = wFC.currentFrameData.energyMax;

            base.Component_PreUpdate();
        }
    }
}