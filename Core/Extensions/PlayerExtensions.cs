using Microsoft.Xna.Framework;
using ProjectFrameWar.Core.Players;
using System;
using System.Linq;
using Terraria;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class PlayerExtensions
    {
        public static bool IsStill(this Player player) => player.velocity == Vector2.Zero;

        public static bool InLiquid(this Player player) => player.wet || player.lavaWet || player.honeyWet || player.shimmerWet;

        public static int CountFromInventory(this Player player, int type)
        {
            int count = 0;

            foreach (Item found in player.inventory.Where(x => x.type == type))
                count += found.stack;

            return count;
        }

        public static void ConsumeFromInventory(this Player player, int type, int amount)
        {
            for (int i = 0; i < player.inventory.Length; i++)
            {
                if (amount <= 0)
                    break;

                if (player.inventory[i].type == type)
                {
                    if (amount > player.inventory[i].stack)
                    {
                        amount -= player.inventory[i].stack;
                        player.inventory[i].stack = 0;
                        player.inventory[i].TurnToAir();
                    }
                    else
                    {
                        player.inventory[i].stack -= amount;
                        amount = 0;
                    }
                }
            }
        }

        public static bool TryEnableComponent<T>(this Player player, Action<T> init = null) where T : PlayerComponent
        {
            if (!player.TryGetModPlayer(out T result))
                return false;

            result.Enabled = true;
            init?.Invoke(result);
            result.OnEnabled();
            return true;
        }

        public static bool TryGetComponent<T>(this Player player, out T result) where T : PlayerComponent
        {
            if (!player.TryGetModPlayer(out T component))
            {
                result = null;
                return false;
            }

            result = component;
            return true;
        }

        public static bool HasComponent<T>(this Player player) where T : PlayerComponent => player.TryGetComponent(out T result) && result.Enabled;

        public static bool TryDisableComponent<T>(this Player player) where T : PlayerComponent
        {
            if (!player.TryGetComponent(out T result))
                return false;

            result.Enabled = false;
            result.OnDisabled();
            return true;
        }
    }
}
