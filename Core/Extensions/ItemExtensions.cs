using ProjectFrameWar.Core.Items;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class ItemExtensions
    {
        public static Item GetItem<T>(string name) where T : ModItem => ModContent.GetContent<T>().First(x => x.Name == name).Item;

        public static string TextIcon(this Item item, Mod mod) => $"[i:{mod.Name}/{item.ModItem.Name}]";

        public static bool TryEnableComponent<T>(this Item item, Action<T> init = null) where T : ItemComponent
        {
            if (!item.TryGetGlobalItem(out T result))
                return false;

            result.Enabled = true;
            init?.Invoke(result);
            result.OnEnabled();
            return true;
        }

        public static bool TryGetComponent<T>(this Item item, out T result) where T : ItemComponent
        {
            if (!item.TryGetGlobalItem(out T component))
            {
                result = null;
                return false;
            }

            result = component;
            return true;
        }

        public static bool HasComponent<T>(this Item item) where T : ItemComponent => item.TryGetComponent(out T result) && result.Enabled;
    }
}
