using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Helpers;
using ProjectFrameWar.Core.Items;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;

namespace ProjectFrameWar.Content.Tiles
{
    internal class FoundryTile : ModTile
    {
        public override string Name => "Foundry";

        public override string Texture => MiscHelpers.CheckTexturePath(Mod, $"{ProjectFrameWar.texPath}/tiles/tile_{Name}");

        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileID.Sets.DisableSmartCursor[Type] = true;
            TileID.Sets.PreventsTileRemovalIfOnTopOfIt[Type] = true;
            TileID.Sets.PreventsTileHammeringIfOnTopOfIt[Type] = true;
            TileID.Sets.AvoidedByMeteorLanding[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.CoordinateHeights = [16, 18];
            TileObjectData.newTile.HookPostPlaceMyPlayer = ModContent.GetInstance<FoundryTileEntity>().Generic_HookPostPlaceMyPlayer;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
            AddMapEntry(Color.AliceBlue);
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            var pos = new Point16(i, j).ToVector2();

            if (!TileEntity.TryGet(i, j, out FoundryTileEntity tile))
                return;

            if (closer && tile.IsWorking)
            {
                if (Main.rand.NextBool(240))
                {
                    string rand = $"{ProjectFrameWar.sfxPath}/tiles/foundry/foundry_Spark{Main.rand.Next(1, 6)}";
                    SoundEngine.PlaySound(new SoundStyle (rand) with { Volume = Main.rand.NextFloat(0.5f, 1f) }, pos * 16);

                    int dustAmount = Main.rand.Next(2, 8);

                    for (int k = 0; k < dustAmount; k++)
                        Dust.NewDust(pos * 16, 10, 10, DustID.TreasureSparkle);
                }
            }
        }

        public override bool RightClick(int i, int j)
        {
            var player = Main.LocalPlayer;

            if (!TileEntity.TryGet(i, j, out FoundryTileEntity foundry))
                return false;

            if (Main.keyState.IsKeyDown(Keys.LeftShift) && foundry.HasCompleted)
            {
                foreach (var item in foundry.blueprintQueue.Where(x => x.TryGetComponent(out BlueprintComponent component) && component.CanCollect).ToList())
                {
                    item.TryGetComponent(out BlueprintComponent blueprint);
                    var result = ItemExtensions.GetItem(blueprint.data.Result).type;
                    Item.NewItem(new EntitySource_Gift(Main.LocalPlayer), Main.LocalPlayer.getRect(), result, Stack: blueprint.data.ResultAmount);
                    foundry.blueprintQueue.Remove(item);
                }

                SoundEngine.PlaySound(new($"{ProjectFrameWar.sfxPath}/tiles/foundry/foundry_Finish"));
            }

            else if (player.HeldItem.HasComponent<BlueprintComponent>())
            {
                if (player.HeldItem.TryGetComponent(out BlueprintComponent component) && component.CanCraft)
                {
                    for (int k = 0; k < component.data.Ingredients.Length; k++)
                    {
                        var itemType = ItemExtensions.GetItem(component.data.Ingredients[k]);
                        player.ConsumeFromInventory(itemType.type, component.data.Requirements[k]);
                    }

                    foundry.blueprintQueue.Add(new Item(player.HeldItem.type));
                    player.HeldItem.TurnToAir();

                    SoundEngine.PlaySound(new($"{ProjectFrameWar.sfxPath}/tiles/foundry/foundry_Start"));
                }
            }

            else
            {

                SoundEngine.PlaySound(new($"{ProjectFrameWar.sfxPath}/ui/ui_Error"));
            }
                

            return true;
        }

        public override void PlaceInWorld(int i, int j, Item item) {
            if (TileEntity.TryGet(i, j, out FoundryTileEntity foundry))
                return;

            foundry.blueprintQueue ??= [];
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY) => ModContent.GetInstance<FoundryTileEntity>().Kill(i, j);
    }

    internal class FoundryTileEntity : ModTileEntity
    {
        public List<Item> blueprintQueue = [];

        public bool IsWorking => blueprintQueue.Any(x => x.TryGetComponent(out BlueprintComponent component) && !component.CanCollect && component.craftTimer > 0);

        public bool HasCompleted => blueprintQueue.Any(x => x.TryGetComponent(out BlueprintComponent component) && component.CanCollect);

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<FoundryTile>();
        }

        public override void SaveData(TagCompound tag) => tag[nameof(blueprintQueue)] = blueprintQueue;

        public override void LoadData(TagCompound tag) => blueprintQueue = tag.Get<List<Item>>(nameof(blueprintQueue));

        public override void Update()
        { 
            if (blueprintQueue is null)
                return;

            foreach (var item in blueprintQueue)
            {
                if (item.TryGetComponent(out BlueprintComponent blueprint))
                {
                    if (!blueprint.CanCollect)
                    {
                        blueprint.craftTimer++;

                        if (blueprint.craftTimer % 60 == 0)
                            Main.NewText($"{item.Name} Time To Craft: {(blueprint.data.CraftTime - blueprint.craftTimer) / 60}s");

                        if (blueprint.craftTimer == blueprint.data.CraftTime)
                            Main.NewText($"[Foundry]: {Main.LocalPlayer.name}, {item.Name} has finished crafting! Come claim it now!");
                    }
                }
            }

            base.Update();
        }

        public override void OnKill()
        {
            blueprintQueue?.Clear();
            blueprintQueue = null;

            base.OnKill();
        }
    }

    public class FoundryTileItem : ModItem
    {
        public override string Name => $"tile_Item_Foundry";

        public override string Texture => MiscHelpers.CheckTexturePath(Mod, $"{ProjectFrameWar.texPath}/tiles/{Name}");

        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<FoundryTile>());
            base.SetDefaults();
        }
    }
}