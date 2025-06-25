using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Systems
{
    internal class WindowTitleSystem : ModSystem
    {
        public bool doTitleChange;

        private int _windowTimer;

        const int WINDOW_TIMER_MAX = 18000;

        private const string LOCAL_KEY = "Mods.ProjectFrameWar.WindowTitles";

        public override void UpdateUI(GameTime gameTime)
        {
            _windowTimer--;

            if (doTitleChange && _windowTimer <= 0)
            {
                Main.instance.Window.Title = "[FRAMEWAR]: " + Language.GetText($"{LOCAL_KEY}.WindowTitle{Main.rand.Next(11)}").Value;
                _windowTimer = WINDOW_TIMER_MAX;
            }

            base.UpdateUI(gameTime);
        }

        public override void Load()
        {
            doTitleChange = true;

            base.Load();
        }
    }
}