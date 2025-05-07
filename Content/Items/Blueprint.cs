using ProjectFrameWar.Core.Dictionaries;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    [Autoload(false)]
    internal class Blueprint(Item result, string key = "") : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"{result.ModItem.Name}_Blueprint";

        public override string Texture => result.ModItem.Texture;

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            var blueprintInfo = BlueprintGuide.blueprintRecipes[key != "" ? key : result.ModItem.Name];

            Item.TryEnableComponent<BlueprintComponent>(x =>
            {
                x.ingredients = blueprintInfo.ingredients;
                x.requirements = blueprintInfo.requirements;
                x.result = result;
                x.resultAmount = blueprintInfo.resultAmount;
            });

            base.SetDefaults();
        }
    }
}
