namespace ProjectFrameWar.Core.Players
{
    internal class WarframeComponent : PlayerComponent
    {
        public FrameData currentFrameData;

        public float abilityStrength;
        public float abilityRange;
        public float abilityEfficiency;
        public float abilityDuration;

        public const float ABILITY_STAT_BASE = 1f;

        public const float ABILITY_EFFICIENCY_MAX = 1.75f;

        public override void Component_PreUpdate()
        {
            abilityStrength = ABILITY_STAT_BASE;
            abilityRange = ABILITY_STAT_BASE;
            abilityEfficiency = ABILITY_STAT_BASE;
            abilityDuration = ABILITY_STAT_BASE;

            if (abilityEfficiency >= ABILITY_EFFICIENCY_MAX)
                abilityEfficiency = ABILITY_EFFICIENCY_MAX;

            UpdateAbilityStats();

            base.Component_PreUpdate();
        }

        public virtual void UpdateAbilityStats() { }
    }
}