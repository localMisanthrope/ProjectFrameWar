using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    [Autoload(false)]
    internal class Relic(RelicComponent.RelicEra era, string series, Item[] relicRewards) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"{era}_{series}_Relic";

        public override string Texture => ProjectFrameWar.placeholderPath;

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.TryEnableComponent<RelicComponent>(x =>
            {
                x.era = era;
                x.relicSeries = series;
                x.relicRewards = relicRewards;
                x.refinement = RelicComponent.RelicRefinement.Intact;
            });

            base.SetDefaults();
        }
    }
}