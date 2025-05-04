using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    [Autoload(false)]
    internal class TileItemBase<T> : ModItem where T : ModTile
    {
        protected override bool CloneNewInstances => true;

        public ModTile tile = TileLoader.GetTile(ModContent.TileType<T>());

        public override string Texture => $"{tile.Name}_Item";
        public override string Name => $"{tile.Name}_Item";

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.createTile = tile.Type;

            base.SetDefaults();
        }
    }
}