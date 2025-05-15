using Microsoft.Xna.Framework.Input;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core
{
    internal class KeybindLoad : ILoadable
    {
        public static ModKeybind CastAbility { get; internal set; }

        public static ModKeybind SwapAbility { get; internal set; }

        public static ModKeybind AbilityAltKey { get; internal set; }

        public void Load(Mod mod)
        {
            CastAbility = KeybindLoader.RegisterKeybind(mod, "Cast Ability", Keys.E);
            SwapAbility = KeybindLoader.RegisterKeybind(mod, "Swap Ability", Keys.F);
        }

        public void Unload()
        {
            CastAbility = null;
            SwapAbility = null;
        }
    }
}