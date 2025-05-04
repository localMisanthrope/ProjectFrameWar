using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectFrameWar.Core.Extensions;
using ReLogic.Content;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal class BlueprintComponent : ItemComponent
    {
        public Item[] ingredients = [null, null, null, null];
        public int[] requirements = [0, 0, 0, 0];
        internal int[] counts = [0, 0, 0, 0];

        public int timeToCraft;
        internal int timer;

        public Item result;
        public int resultAmount;

        public Asset<Texture2D> blueprintBack = ModContent.Request<Texture2D>($"{ProjectFrameWar.texPath}/blueprint_back", AssetRequestMode.AsyncLoad);

        public const string LOCAL_KEY = "Mods.ProjectFrameWar.Blueprints";

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips[0].Text = $"{result.Name} {Language.GetTextValue($"{LOCAL_KEY}.Blueprint_Word")}";

            for (int i = 0; i < ingredients.Length; i++)
            {
                if (ingredients[i] is null)
                    continue;

                tooltips.Add(new(Mod, $"BlueprintIngredient_{i}",
                    $"{ingredients[i].TextIcon(Mod)} {ingredients[i].Name}: {counts[i]} / {requirements[i]}"));
            }

            tooltips.Add(new(Mod, "ResultLine", $"Result: {result.TextIcon(Mod)} {result.Name} ({resultAmount})"));

            base.Component_ModifyTooltips(item, tooltips);
        }

        public override bool Component_PreDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(blueprintBack.Value, position, frame, drawColor, 0f, origin, scale * 1.25f, SpriteEffects.None, 1);

            return true;
        }

        public override void Component_UpdateInventory(Item item, Player player)
        {
            for (int i = 0; i < counts.Length; i++)
                counts[i] = player.CountFromInventory(ingredients[i].type);

            base.Component_UpdateInventory(item, player);
        }
    }
}