using ProjectFrameWar.Core.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Extensions
{
    internal static class ItemExtensions
    {
        /// <summary>
        /// Attempts to get an item by name from ModContent.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Item GetItem(string name) => ModContent.GetContent<ModItem>().First(x => x.Name == name).Item;
        /// <summary>
        /// Attempts to get an item by name more directly by its type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Item GetItem<T>(string name) where T : ModItem => ModContent.GetContent<T>().First(x => x.Name == name).Item;

        /// <summary>
        /// Converts an item into its text icon.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="stack"></param>
        /// <returns></returns>
        public static string TextIcon(this Item item, int stack = 0)
            => $"[i" + ((stack == 0) ? ":" : $"/{stack}:") + ((item.ModItem is not null) ? $"{item.ModItem.Mod.Name}/{item.ModItem.Name}" : $"{item.type}") + "]";

        /// <summary>
        /// Attempts to enable a component directly by its name.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool TryEnableComponent(this Item item, string name)
        {
            var result = item.GetAllComponents().First(x => x.Name == $"{name}Component");
            if (result is null)
                return false;

            result.Enabled = true;
            result.OnEnabled(item);
            return true;
        }

        /// <summary>
        /// Attempts to disable a component directly by its name.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool TryDisableComponent(this Item item, string name)
        {
            var result = item.GetAllComponents().First(x => x.Name == $"{name}Component");
            if (result is null)
                return false;

            result.OnDisabled(item);
            result.Enabled = false;
            return true;
        }

        public static bool TryEnableComponent<T>(this Item item, Action<T> init = null) where T : ItemComponent
        {
            if (!item.TryGetGlobalItem(out T result))
                return false;

            result.Enabled = true;
            init?.Invoke(result);
            result.OnEnabled(item);
            return true;
        }

        /// <summary>
        /// Returns a list of all ItemComponents on the item.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<ItemComponent> GetAllComponents(this Item item)
        {
            var list = new List<ItemComponent>();

            foreach (var global in item.Globals)
                if (global is ItemComponent component)
                    list.Add(component);

            return list;
        }

        /// <summary>
        /// Safely attempts to get the ItemComponent instance on the item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="result"></param>
        /// <returns>Whether or not the component exists.</returns>
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

        /// <summary>
        /// Checks whether the ItemComponent instance on the item exists and is enabled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool HasComponent<T>(this Item item) where T : ItemComponent => item.TryGetComponent(out T result) && result.Enabled;
        /// <summary>
        /// Checks whether the ItemComponent instance on the item exists and is enabled.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="component">The component to be used.</param>
        /// <returns>Whether or not the component is enabled.</returns>
        public static bool HasComponent<T>(this Item item, out T component) where T : ItemComponent
        {
            if (!item.TryGetComponent(out component))
                return false;

            return component.Enabled;
        }

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
