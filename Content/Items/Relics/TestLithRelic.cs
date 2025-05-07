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
                    { ItemExtensions.GetItem<Resource>("Resource_Ferrite"), RelicComponent.ItemRarity.Common },
                    { ItemExtensions.GetItem<Resource>("Resource_AlloyPlate"), RelicComponent.ItemRarity.Common },
                    { ItemExtensions.GetItem<Resource>("Resource_Rubedo"), RelicComponent.ItemRarity.Common },
                    { ItemExtensions.GetItem<Resource>("Resource_NeuralSensor"), RelicComponent.ItemRarity.Uncommon },
                    { ItemExtensions.GetItem<Resource>("Resource_VoidTrace"), RelicComponent.ItemRarity.Uncommon },
                    { ItemExtensions.GetItem<FramePart>("Neuroptics_Excalibur"), RelicComponent.ItemRarity.Rare }
                };
                x.refinement = RelicComponent.RelicRefinement.Intact;
                x.era = RelicComponent.RelicEra.Lith;
                x.relicSeries = "E1";
            });

            base.SetDefaults();
        }
    }
}