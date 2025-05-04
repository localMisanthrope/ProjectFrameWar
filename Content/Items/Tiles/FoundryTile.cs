using ProjectFrameWar.Core.TileEntities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace ProjectFrameWar.Content.Items.Tiles
{
    internal class FoundryTile : ModTile
    {
        public override string Texture => "ProjectFrameWar/res/texture/placeholder";

        public static string MapHoverText(string name, int i, int j)
        {
            //Get tile entity.
            //Transmit current data onto map text.

            return " ";
        }

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;

            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
            TileID.Sets.PreventsTileReplaceIfOnTopOfIt[Type] = true;
            TileID.Sets.AvoidedByMeteorLanding[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);

            AddMapEntry(new(), CreateMapEntryName(), MapHoverText);

            base.SetStaticDefaults();
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            ModContent.GetInstance<FoundryTileEntity>().Kill(i, j);

            base.KillMultiTile(i, j, frameX, frameY);
        }
    }
}