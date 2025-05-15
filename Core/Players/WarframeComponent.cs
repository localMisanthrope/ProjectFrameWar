using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Extensions;
using Terraria;

namespace ProjectFrameWar.Core.Players
{
    internal class WarframeComponent : PlayerComponent
    {
        public string frameName;

        public FrameData currentFrameData;

        public float abilityStrength;
        public float abilityRange;
        public float abilityEfficiency;
        public float abilityDuration;

        public const float ABILITY_STAT_BASE = 1f;

        public const float ABILITY_EFFICIENCY_MAX = 1.75f;

        public override void OnEnabled()
        {
            //To-do: On Warframe enabled
            //  Enable the Ability Component
            //  Assign each slot to the corresponding ability.

            Player.armor[0] = ItemExtensions.GetItem<WarframeItem_HeadBase>($"{frameName}Helmet");
            Player.armor[1] = ItemExtensions.GetItem<WarframeItem_ChestBase>($"{frameName}Chest");
            Player.armor[2] = ItemExtensions.GetItem<WarframeItem_LegsBase>($"{frameName}Legs");

            Player.TryEnableComponent<EnergyComponent>();
            Player.TryEnableComponent<HealthComponent>();
            Player.TryEnableComponent<ShieldsComponent>();

            base.OnEnabled();
        }

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