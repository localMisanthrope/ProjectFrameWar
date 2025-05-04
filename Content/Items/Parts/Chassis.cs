using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items.Parts
{
    [Autoload(false)]
    internal class Chassis(string frameName) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Texture => "ProjectFrameWar/res/texture/warframes/part_Chassis";

        private readonly string frameName = frameName;

        public override string Name => $"Chassis_{frameName}";

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.TryEnableComponent<FramePartComponent>(x =>
            {
                x.type = FramePartComponent.PartType.CHASSIS;
                x.frameName = frameName;
            });

            base.SetDefaults();
        }
    }
}
