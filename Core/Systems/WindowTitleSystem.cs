using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Systems
{
    internal class WindowTitleSystem : ModSystem
    {
        public List<LocalizedText> titles;

        public int titlesCount;

        internal int timer;

        public const string LOCAL_KEY = "Mods.ProjectFrameWar.WindowTitles";

        public void GenRandWindowTitle() => Main.instance.Window.Title = $"[WARFRAME]: {titles[Main.rand.Next(0, titlesCount)].Value}";

        public override void UpdateUI(GameTime gameTime)
        {
            timer--;

            if (timer <= 0)
                GenRandWindowTitle();

            base.UpdateUI(gameTime);
        }

        public override void Load()
        {
            titles = [];
            titlesCount = 11;

            for (int i = 1; i == titlesCount; i++)
                titles.Add(Language.GetText($"{LOCAL_KEY}.WindowTitle{i}"));

            GenRandWindowTitle();

            timer = 18000;

            base.Load();
        }
    }
}