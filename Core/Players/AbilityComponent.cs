using Terraria.GameInput;

namespace ProjectFrameWar.Core.Players
{
    internal class AbilityComponent : PlayerComponent
    {
        public Ability CurrentAbility => abilities[selected];

        public int selected;

        public Ability[] abilities;

        public override void Component_ProcessInput(TriggersSet triggersSet)
        {
            if (KeybindLoad.SwapAbility.JustPressed)
            {
                selected++;

                if (selected >= abilities.Length)
                    selected = 0;
            }

            if (KeybindLoad.CastAbility.JustPressed && CurrentAbility.CanCast(Player))
                CurrentAbility.OnCast(Player);

            base.Component_ProcessInput(triggersSet);
        }

        public override void Component_PreUpdate()
        {
            foreach (var ability in abilities)
                if (ability.isActive)
                    ability.UpdateEffect(Player);

            base.Component_PreUpdate();
        }

        public override void Component_UpdateDead()
        {
            foreach (var ability in abilities)
                ability.isActive = false;

            base.Component_UpdateDead();
        }
    }
}