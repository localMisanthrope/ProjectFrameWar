using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    [Autoload(false)]
    internal class FramePart(FramePartComponent.PartType type, string frameName) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Name => $"{type}_{frameName}";

        public override string Texture => $"{Mod.Name}/res/texture/warframes/part_{type}" + (frameName.Contains("_Prime") ? "_Prime" : "");

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.TryEnableComponent<FramePartComponent>(x =>
            {
                x.type = type;
                x.frameName = frameName;
            });

            base.SetDefaults();
        }
    }
}
