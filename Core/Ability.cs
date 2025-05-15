using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Players;
using Terraria;
using Terraria.Localization;

namespace ProjectFrameWar.Core
{
    internal class Ability
    {
        public bool isActive;

        public int energyCost;

        public LocalizedText abilityName;
        public LocalizedText abilityDesc;

        public static WarframeComponent WarframeComp(Player player) 
            => !player.TryGetComponent(out WarframeComponent wfC) ? null : wfC;

        public static EnergyComponent EnergyComp(Player player)
            => !player.TryGetComponent(out EnergyComponent eC) ? null : eC;

        public virtual bool CanCast(Player player) 
            => player.TryGetComponent(out EnergyComponent energy) && energy.energyCurrent >= energyCost;

        public virtual void OnCast(Player player) { }

        public virtual void OnRecast(Player player) { }

        public virtual void SetDefaults() { }

        public virtual void UpdateEffect(Player player) { }
    }
}