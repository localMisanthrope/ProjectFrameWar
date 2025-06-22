using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Extensions;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProjectFrameWar.Core.Items
{
    internal enum RelicEra { Lith, Meso, Neo, Axi }

    internal enum RelicRewardRarity { Common, Uncommon, Rare }

    internal enum RelicState { Intact, Exceptional, Flawless, Radiant }

    internal class RelicComponent : ItemComponent
    {
        public RelicData data;

        public RelicState refinement;

        internal float[] rewardPercentages;

        public int RefineCost => 25 * (int)Math.Pow(2, (int)refinement);

        private static RelicRewardRarity GetItemRarity(int index) => index switch
        {
            3 => RelicRewardRarity.Uncommon,
            4 => RelicRewardRarity.Uncommon,
            5 => RelicRewardRarity.Rare,
            _ => RelicRewardRarity.Common
        };

        public override bool Component_CanRCLK(Item item) => true;

        public override void Component_OnRCLK(Item item, Player player)
        {
            if (Main.keyState.IsKeyDown(Keys.LeftShift))
                DispenseRelicReward(player);

            else
            {
                int count = player.CountFromInventory(ItemExtensions.GetItem("resource_VoidTraces").type);
                if (count >= RefineCost && refinement < RelicState.Radiant)
                {
                    player.ConsumeFromInventory(ItemExtensions.GetItem("resource_VoidTraces").type, RefineCost);
                    refinement += 1;
                }
                else
                {

                }

                item.stack++;
            }

            base.Component_OnRCLK(item, player);
        }

        public void DispenseRelicReward(Player player)
        {
            int[] percentageArr = new int[data.rewards.Length];
            for (int i = 0; i < percentageArr.Length; i++)
                percentageArr[i] = (int)(rewardPercentages[(int)GetItemRarity(i)] * 100);

            int result = Main.rand.Next(0, percentageArr.Sum());
            int weightCheck = 0;

            for (int i = 0; i < percentageArr.Length; i++)
            {
                weightCheck += percentageArr[i];
                if (result > weightCheck)
                    continue;

                else
                {
                    player.QuickSpawnItem(new EntitySource_Gift(player), ItemExtensions.GetItem(data.rewards[i]));
                    player.QuickSpawnItem(new EntitySource_Gift(player), ItemExtensions.GetItem("resource_VoidTraces"), Main.rand.Next(6, 26));
                    break;
                }
            }
        }

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string state = Language.GetText($"{LOCAL_KEY}.RelicState.{refinement}").Value;
            string nextState = Language.GetText($"{LOCAL_KEY}.RelicState.{refinement.NextEnum()}").Value;

            item.SetNameOverride($"{data.era} {data.name} Relic ({state})");

            tooltips.Add(new(Mod, "DescriptionLine", Language.GetTextValue($"{LOCAL_KEY}.RelicDescription")));

            for (int i = 0; i < data.rewards.Length; i++)
            {
                float percentage = rewardPercentages[(int)GetItemRarity(i)];

                Item reward = ItemExtensions.GetItem(data.rewards[i]);

                tooltips.Add(new(Mod, $"RewardLine_{i}", $"{reward.TextIcon()} {reward.Name} ({percentage.FormatAsPercent(2)}%)")
                {
                    OverrideColor = GetItemRarity(i) switch 
                    { 
                        RelicRewardRarity.Common => Color.SandyBrown,
                        RelicRewardRarity.Uncommon => Color.Silver,
                        RelicRewardRarity.Rare => Color.Goldenrod
                    }
                });
            }

            if (refinement is not RelicState.Radiant)
                tooltips.Add(new(Mod, "RefineStatusLine", "\n" + Language.GetText($"{LOCAL_KEY}.RelicRefinable").Format(state, nextState)
                    + $" {ItemExtensions.GetItem("resource_VoidTraces").TextIcon()} ({RefineCost})"));
        }

        public override void Component_UpdateInventory(Item item, Player player) => rewardPercentages = refinement switch
        {
            RelicState.Intact => [0.2533f, 0.11f, 0.02f],
            RelicState.Exceptional => [0.2333f, 0.13f, 0.04f],
            RelicState.Flawless => [0.2f, 0.17f, 0.06f],
            RelicState.Radiant => [0.1667f, 0.2f, 0.1f],
            _ => [0.2533f, 0.11f, 0.02f]
        };

        public override bool Component_PreDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) => false;
        public override bool Component_PreDrawWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) => false;

        public override void Component_PostDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> sheet = ModContent.Request<Texture2D>($"ProjectFrameWar/res/texture/relic_{data.era}_Sheet");
            var sheetFrame = sheet.Frame(horizontalFrames: 4, frameX: (int)refinement);
            var sheetOrig = sheetFrame.Size() / 2;

            spriteBatch.Draw(sheet.Value, position, sheetFrame, drawColor, 0, sheetOrig, scale, SpriteEffects.None, 0);
        }

        public override void Component_SaveData(Item item, TagCompound tag) => tag[nameof(refinement)] = (int)refinement;
        public override void Component_LoadData(Item item, TagCompound tag) => refinement = (RelicState)tag.GetInt(nameof(refinement));
    }
}