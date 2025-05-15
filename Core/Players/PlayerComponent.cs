using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameInput;

namespace ProjectFrameWar.Core.Players
{
    internal class PlayerComponent : ModPlayer
    {
        public bool Enabled { get; set; }

        public const string LocalKey = "Mods.ProjectFrameWar.PlayerComponents";

        public virtual void OnEnabled() { }

        public virtual void OnDisabled() { }

        public virtual void Component_Kill(double damage, int hitDir, bool pvp, PlayerDeathReason reason) { }

        public virtual void Component_LoadData(TagCompound tag) { }

        public virtual void Component_Load() { }

        public virtual void Component_SaveData(TagCompound tag) { }

        public virtual void Component_UpdateDead() { }

        public virtual void Component_Unload() { }

        public virtual void Component_PostUpdate() { }

        public virtual void Component_PreUpdate() { }

        public virtual void Component_PreUpdateMovement() { }

        public virtual void Component_ProcessInput(TriggersSet triggersSet) { }

        public sealed override void AnglerQuestReward(float rareMultiplier, List<Item> rewardItems) { return; }

        public sealed override void ArmorSetBonusActivated() { return; }

        public sealed override void ArmorSetBonusHeld(int holdTime) { return; }

        public sealed override void GetDyeTraderReward(List<int> rewardPool) { return; }

        public sealed override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        { if (Enabled) Component_Kill(damage, hitDirection, pvp, damageSource); }

        public sealed override void Load()
        { if (Enabled) Component_Load(); }

        public sealed override void LoadData(TagCompound tag)
        { if (Enabled) Component_LoadData(tag); }

        public sealed override void SaveData(TagCompound tag)
        { if (Enabled) Component_SaveData(tag); }

        public sealed override void UpdateDead()
        { if (Enabled) Component_UpdateDead(); }

        public sealed override void Unload()
        { if (Enabled) Component_Unload(); }

        public sealed override void PostUpdate()
        { if (Enabled) Component_PostUpdate(); }

        public sealed override void PreUpdate()
        { if (Enabled) Component_PreUpdate(); }

        public sealed override void PreUpdateMovement()
        { if (Enabled) Component_PreUpdateMovement(); }

        public sealed override void ProcessTriggers(TriggersSet triggersSet)
        { if (Enabled) Component_ProcessInput(triggersSet); }
    }
}