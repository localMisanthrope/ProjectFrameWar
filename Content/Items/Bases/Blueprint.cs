using ProjectFrameWar.Core.Dictionaries;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    [Autoload(false)]
    internal partial class Blueprint(Item result, string key = "") : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"{result.ModItem.Name}_Blueprint";

        public override string Texture => result.ModItem.Texture;

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.TryEnableComponent<BlueprintComponent>(x =>
            {
                x.blueprint = BlueprintData.blueprintRecipes[key != "" ? key : result.ModItem.Name];
                x.result = result;
            });

            base.SetDefaults();
        }
    }
}
