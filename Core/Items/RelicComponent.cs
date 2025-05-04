using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectFrameWar.Content.Items;
using ProjectFrameWar.Core.Extensions;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Globalization;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProjectFrameWar.Core.Items
{
    internal class RelicComponent : ItemComponent
    {
        public Dictionary<Item, ItemRarity> relicRewards;

        internal float[] rarityPercentages;

        public int voidTraceReward;

        public Item vtRef = ItemExtensions.GetItem<Resource>("Resource_VoidTrace");

        public RelicEra era;

        public RelicRefinement refinement;

        public string relicSeries;

        private const string LOCAL_KEY = "Mods.ProjectFrameWar.Relics";

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string refineState = Language.GetText($"{LOCAL_KEY}.Relic_Refine_{refinement}").Value;
            string nextRefine = Language.GetText($"{LOCAL_KEY}.Relic_Refine_{refinement.NextEnum()}").Value;

            tooltips[0].Text = $"{era.ToString().Titleize()} {item.Name} {relicSeries} ({refineState})";

            foreach (Item reward in relicRewards.Keys)
            {
                Color rewardColor = relicRewards[reward] switch
                {
                    ItemRarity.COMMON => Color.Brown,
                    ItemRarity.UNCOMMON => Color.Silver,
                    ItemRarity.RARE => Color.Goldenrod,
                    _ => Color.Brown
                };

                rarityPercentages = refinement switch
                {
                    RelicRefinement.INTACT => [0.2533f, 0.11f, 0.02f],
                    RelicRefinement.EXCEPTIONAL => [0.2333f, 0.13f, 0.04f],
                    RelicRefinement.FLAWLESS => [0.2f, 0.17f, 0.06f],
                    RelicRefinement.RADIANT => [0.1667f, 0.2f, 0.1f],
                    _ => [0.2533f, 0.11f, 0.02f]
                };

                float getPercentage = rarityPercentages[(int)relicRewards[reward]] * 100;

                tooltips.Add(new(Mod,
                    "RelicReward",
                    $"{reward.TextIcon(Mod)} {reward.Name} " +
                    $"({getPercentage.ToString("N", new NumberFormatInfo() { NumberDecimalDigits = 2})}%)") 
                { OverrideColor = rewardColor });
            }

            if (refinement is not RelicRefinement.RADIANT)
                tooltips.Add(new(Mod,
                    "UpgradeLine",
                    $"{Language.GetText($"{LOCAL_KEY}.Relic_Refinement_Option").Format(refineState, nextRefine)}" +
                    $"{vtRef.TextIcon(Mod)} ({25 * (int)Math.Pow(2, (int)refinement)})"));

            base.Component_ModifyTooltips(item, tooltips);
        }

        public override bool Component_CanRCLK(Item item) => true;

        public override void Component_OnRCLK(Item item, Player player)
        {
            int count = player.CountFromInventory(vtRef.type);

            Mod.Logger.Debug($"VT Count: {count}");

            int traceReq = 25 * (int)Math.Pow(2, (int)refinement);

            Mod.Logger.Debug($"VT Req: {traceReq}");

            if (count >= traceReq)
            {
                player.ConsumeFromInventory(vtRef.type, traceReq);

                refinement += 1;
                //To-do: Play custom sound that changes with refinement level (for local player only).
            }

            else
            {
                Mod.Logger.Debug($"Failed to refine. Requirement not met. Count: {count}, Req: {traceReq}");
                //To-do: Play custom sound for when the item fails to be refined.
            }

            item.stack++;

            base.Component_OnRCLK(item, player);
        }

        public override bool Component_PreDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) => false;
        public override bool Component_PreDrawWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) => false;

        public override void Component_PostDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Asset<Texture2D> sheet = ModContent.Request<Texture2D>($"ProjectFrameWar/res/texture/relic_{era.ToString().ToLower()}_sheet");
            var relicFrame = sheet.Frame(horizontalFrames: 4, frameX: (int)refinement);
            var relicOrig = relicFrame.Size() / 2;

            spriteBatch.Draw(sheet.Value, position, relicFrame, drawColor, 0, relicOrig, scale, SpriteEffects.None, 0);

            base.Component_PostDrawInv(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public override void Component_SaveData(Item item, TagCompound tag)
        {
            tag[nameof(refinement)] = (int)refinement;

            base.Component_SaveData(item, tag);
        }

        public override void Component_LoadData(Item item, TagCompound tag)
        {
            refinement = (RelicRefinement)tag.GetInt(nameof(refinement));

            base.Component_LoadData(item, tag);
        }

        public enum RelicRefinement
        {
            INTACT,
            EXCEPTIONAL,
            FLAWLESS,
            RADIANT
        }

        public enum ItemRarity
        {
            COMMON,
            UNCOMMON,
            RARE
        }

        public enum RelicEra
        {
            LITH,
            MESO,
            NEO,
            AXI
        }
    }
}