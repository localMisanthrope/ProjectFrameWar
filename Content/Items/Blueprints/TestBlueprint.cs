using ProjectFrameWar.Content.Items.Parts;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Blueprints
{
    internal class TestBlueprint : ModItem
    {
        public override string Texture => "ProjectFrameWar/res/texture/placeholder";

        public override void SetDefaults()
        {
            Item.TryEnableComponent<BlueprintComponent>(x =>
            {
                x.ingredients = [
                    ItemExtensions.GetItem<Resource>("Resource_AlloyPlate"),
                    ItemExtensions.GetItem<Resource>("Resource_Ferrite"),
                    ItemExtensions.GetItem<Resource>("Resource_Rubedo"),
                    ItemExtensions.GetItem<Neuroptics>("Neuroptics_Excalibur"),
                    ];

                x.requirements = [10, 10, 10, 1];

                x.result = ItemExtensions.GetItem<Resource>("Resource_NeuralSensor");
                x.resultAmount = 1;
            });

            base.SetDefaults();
        }
    }
}