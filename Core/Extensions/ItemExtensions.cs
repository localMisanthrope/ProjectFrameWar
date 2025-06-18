using ProjectFrameWar.Core.Items;
using System;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class ItemExtensions
    {
        public static Item GetItem(string name) => ModContent.GetContent<ModItem>().First(x => x.Name == name).Item;
        public static Item GetItem<T>(string name) where T : ModItem => ModContent.GetContent<T>().First(x => x.Name == name).Item;

        public static string TextIcon(this Item item, int stack = 0)
            => $"[i" + ((stack == 0) ? ":" : $"/{stack}:") + ((item.ModItem is not null) ? $"{item.ModItem.Mod.Name}/{item.ModItem.Name}" : $"{item.type}") + "]";

        public static bool TryEnableComponent<T>(this Item item, Action<T> init = null) where T : ItemComponent
        {
            if (!item.TryGetGlobalItem(out T result))
                return false;

            result.Enabled = true;
            init.Invoke(result);
            result.OnEnabled(item);
            return true;
        }

        public static bool TryGetComponent<T>(this Item item, out T result) where T : ItemComponent
        {
            if (!item.TryGetGlobalItem(out T res))
            {
                result = null;
                return false;
            }

            result = res;
            return true;
        }

        public static bool HasComponent<T>(this Item item) where T : ItemComponent => item.TryGetComponent(out T result) && result.Enabled;

        public static bool DisableComponent<T>(this Item item) where T : ItemComponent
        {
            if (!item.TryGetComponent(out T result))
                return false;

            result.OnDisabled(item);
            result.Enabled = false;
            return true;
        }
    }
}
