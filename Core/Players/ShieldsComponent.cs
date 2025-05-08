using Microsoft.Xna.Framework;
using ProjectFrameWar.Core.Extensions;
using System;
using Terraria;

namespace ProjectFrameWar.Core.Players
{
    internal class ShieldsComponent : PlayerComponent
    {
        public bool shieldsBroken;

        public bool hasOvershields;

        public int shieldsCurrent;

        public int maxShields;

        public int overShields;

        public int shieldGateTimer;

        public int shieldRegenBuffer;

        public int shieldRechargeRate;

        public const int MAX_OVERSHIELDS = 1200;

        public void AddShields(int amount, bool validForOvershield = false)
        {
            var diff = amount;

            if (shieldsCurrent + amount > maxShields)
                diff = maxShields - shieldsCurrent;

            shieldsCurrent = Math.Clamp(shieldsCurrent + diff, 0, maxShields);
            CombatText.NewText(Player.getRect(), Color.Blue, amount);

            if (validForOvershield)
            {
                overShields = Math.Clamp(overShields + (amount - diff), 0, MAX_OVERSHIELDS);
                CombatText.NewText(Player.getRect(), Color.Purple, amount - diff);
            }
        }

        public void DamageShields(int amount)
        {

        }

        public override void Component_PreUpdate()
        {
            if (!Player.TryGetComponent(out WarframeComponent wFC))
                return;

            shieldsBroken = false;
            hasOvershields = false;
            maxShields = wFC.currentFrameData.shieldsMax;

            shieldRechargeRate = (15 + (int)(0.05 * maxShields)) / 60;

            if (shieldsCurrent <= 0)
                shieldsBroken = true;

            if (overShields > 0)
                hasOvershields = true;

            if (shieldRegenBuffer > 0)
            {
                shieldRegenBuffer--;
                if (shieldRegenBuffer <= 0)
                {
                    shieldsCurrent = Math.Clamp(shieldsCurrent + shieldRechargeRate, 0, maxShields);

                }
            }

            base.Component_PreUpdate();
        }
    }
}