using Microsoft.Xna.Framework;
using ProjectFrameWar.Core.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class PlayerExtensions
    {
        /// <summary>
        /// Whether or not the player is standing still.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool IsStill(this Player player) => player.velocity == Vector2.Zero;

        /// <summary>
        /// Whether or not the player is in any liquid (does not included modded liquids).
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool InLiquid(this Player player) => player.wet || player.lavaWet || player.honeyWet || player.shimmerWet;

        /// <summary>
        /// Gets a count of all instances of a specific item from the inventory.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="type"></param>
        /// <returns>Total count of the specific item</returns>
        public static int CountFromInventory(this Player player, int type)
        {
            int count = 0;

            foreach (Item found in player.inventory.Where(x => x.type == type))
                count += found.stack;

            return count;
        }

        /// <summary>
        /// Consumes a set amount of an item from the player's inventory.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="type"></param>
        /// <param name="amount"></param>
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

        public static IEnumerable<NPC> SurroundingNPCs(this Player player, float radius) => Main.npc.Where(x => x.active && x.WithinRange(player.position, radius));

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
