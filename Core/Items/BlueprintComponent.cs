using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Extensions;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

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

        internal int[] counts;

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            for (int i = 0; i < data.ingredients.Length; i++)
            {
                if (data.ingredients[i] is null)
                    continue;

                var ingr = ItemExtensions.GetItem(data.ingredients[i]);
                tooltips.Add(new(Mod, $"Ingredient_{i}", $"{ingr.TextIcon()} {ingr.Name}: {counts[i]}/{data.requirements[i]}"));
            }

            tooltips.Add(new(Mod, "ResultLine", $"\n {Language.GetText($"{LOCAL_KEY}.BlueprintResult").Format(ItemExtensions.GetItem(data.result).TextIcon(), data.resultAmount)}"));
        }

        public override void Component_PostDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            //To-Do: Draw reward.
        }

        public override void Component_UpdateInventory(Item item, Player player)
        {
            for (int i = 0; i < counts.Length; i++)
                counts[i] = player.CountFromInventory(ItemExtensions.GetItem(data.ingredients[i]).type);
        }
    }
}
