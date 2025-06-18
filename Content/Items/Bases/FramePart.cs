using ProjectFrameWar.Core;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Bases
{
    [Autoload(false)]
    internal class FramePart(FrameData data, PartType type) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"part_{type}_{data.name}";

        public override string Texture => $"{ProjectFrameWar.texPath}/warframes/part_{type}";

        public override void SetDefaults()
        {
            Item.TryEnableComponent<FramePartComponent>(x =>
            {
                x.data = data;
                x.type = type;
            });

            base.SetDefaults();
        }
    }
}