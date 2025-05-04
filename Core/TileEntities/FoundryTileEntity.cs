using ProjectFrameWar.Content.Items.Tiles;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProjectFrameWar.Core.TileEntities
{
    internal class FoundryTileEntity : ModTileEntity
    {
        public Queue<Item> inProgressBlueprints;

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<FoundryTile>();
        }

        public override void LoadData(TagCompound tag)
        {

            base.LoadData(tag);
        }

        public override void SaveData(TagCompound tag)
        {

            base.SaveData(tag);
        }

        public override void Update()
        {
            base.Update();
        }
    }
}