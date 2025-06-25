using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Extensions;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProjectFrameWar.Core.Items
{
    internal enum BlueprintCategory
    {
        Misc,
        Module,
        Warframe,
        Weapon
    }

    internal class BlueprintComponent : ItemComponent
    {
        internal BlueprintData data;

        public bool CanCraft => CanCraftMe();

        internal int[] counts;

        public int craftTimer;

        public bool CanCollect => craftTimer >= data.CraftTime;

        internal BlueprintCategory category;

        internal bool CanCraftMe()
        {
            int count = 0;

            for (int i = 0; i < data.Ingredients.Length; i++)
            {
                if (counts[i] >= data.Requirements[i])
                    count++;
            }

            return count >= data.Ingredients.Length;
        }

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < data.Ingredients.Length; i++)
            {
                if (data.Ingredients[i] is null)
                    continue;

                var ingr = ItemExtensions.GetItem(data.Ingredients[i]);
                tooltips.Add(new(Mod, $"Ingredient_{i}", $"{ingr.TextIcon()} {ingr.Name}: {counts[i]}/{data.Requirements[i]}"));
            }

            tooltips.Add(new(Mod, "ResultLine", $"\n {Language.GetText($"{LOCAL_KEY}.BlueprintResult").Format(ItemExtensions.GetItem(data.Result).TextIcon(), data.ResultAmount)}"));
        }

        public override void Component_PostDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //To-Do: Draw reward.
        }

        public override void Component_SaveData(Item item, TagCompound tag) => tag[nameof(craftTimer)] = craftTimer;
        public override void Component_LoadData(Item item, TagCompound tag) => craftTimer = tag.GetInt(nameof(craftTimer));

        public override void Component_UpdateInventory(Item item, Player player)
        {
            for (int i = 0; i < counts.Length; i++)
            {
                counts[i] = player.CountFromInventory(ItemExtensions.GetItem(data.Ingredients[i]).type);
            }
               
        }
    }
}
