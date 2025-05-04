using ProjectFrameWar.Content.Items.Parts;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Relics
{
    internal class TestLithRelic : ModItem
    {
        public override string Texture => "ProjectFrameWar/res/texture/placeholder";

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.TryEnableComponent<RelicComponent>(x =>
            {
                x.relicRewards = new()
                {
                    { ItemExtensions.GetItem<Resource>("Resource_Ferrite"), RelicComponent.ItemRarity.COMMON },
                    { ItemExtensions.GetItem<Resource>("Resource_AlloyPlate"), RelicComponent.ItemRarity.COMMON },
                    { ItemExtensions.GetItem<Resource>("Resource_Rubedo"), RelicComponent.ItemRarity.COMMON },
                    { ItemExtensions.GetItem<Resource>("Resource_NeuralSensor"), RelicComponent.ItemRarity.UNCOMMON },
                    { ItemExtensions.GetItem<Resource>("Resource_VoidTrace"), RelicComponent.ItemRarity.UNCOMMON },
                    { ItemExtensions.GetItem<Neuroptics>("Neuroptics_Excalibur"), RelicComponent.ItemRarity.RARE }
                };
                x.refinement = RelicComponent.RelicRefinement.INTACT;
                x.era = RelicComponent.RelicEra.LITH;
                x.relicSeries = "E1";
            });

            base.SetDefaults();
        }
    }
}