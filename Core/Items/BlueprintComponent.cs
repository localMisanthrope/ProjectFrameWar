using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectFrameWar.Core.Extensions;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal class BlueprintComponent : ItemComponent
    {
        public BlueprintRecipe blueprint;

        private int[] _counts = [0, 0, 0, 0];

        public Item result;

        public Asset<Texture2D> blueprintBack = ModContent.Request<Texture2D>($"{ProjectFrameWar.texPath}/blueprint_back", AssetRequestMode.AsyncLoad);

        public const string LOCAL_KEY = "Mods.ProjectFrameWar.Blueprints";

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips[0].Text = $"{result.Name} {Language.GetTextValue($"{LOCAL_KEY}.Blueprint_Word")}";

            for (int i = 0; i < blueprint.ingredients.Length; i++)
            {
                if (blueprint.ingredients[i] is null)
                    continue;

                tooltips.Add(new(Mod, $"BlueprintIngredient_{i}",
                    $"{blueprint.ingredients[i].TextIcon(Mod)} {blueprint.ingredients[i].Name}: {_counts[i]} / {blueprint.requirements[i]}"));
            }

            tooltips.Add(new(Mod, "ResultLine", $"Result: {result.TextIcon(Mod)} {result.Name} ({blueprint.resultAmount})"));

            base.Component_ModifyTooltips(item, tooltips);
        }

        public override bool Component_PreDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            spriteBatch.Draw(blueprintBack.Value, position, frame, drawColor, 0f, origin, 1.25f, SpriteEffects.None, 0f);
            return true;
        }

        public override void Component_UpdateInventory(Item item, Player player)
        {
            for (int i = 0; i < _counts.Length; i++)
                _counts[i] = player.CountFromInventory(blueprint.ingredients[i].type);

            base.Component_UpdateInventory(item, player);
        }
    }
}