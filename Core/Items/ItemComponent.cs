using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace ProjectFrameWar.Core.Items
{
    internal abstract class ItemComponent : GlobalItem
    {
        public bool Enabled { get; set; }

        public sealed override bool InstancePerEntity { get; } = true;

        public const string LOCAL_KEY = "Mods.ProjectFrameWar.ItemComponents";

        public virtual void OnEnabled(Item item) { }

        public virtual void OnDisabled(Item item) { }

        public virtual bool Component_CanPickup(Item item, Player player) => true;

        public virtual bool Component_PreDrawWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI) => true;

        public virtual bool Component_PreDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) => true;

        public virtual bool Component_CanRCLK(Item item) => false;

        public virtual bool Component_OnPickup(Player player, Item item) => true;

        public virtual void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips) { }

        public virtual void Component_OnConsumeItem(Item item, Player player) { }

        public virtual void Component_OnRCLK(Item item, Player player) { }

        public virtual void Component_PostDrawWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI) { }

        public virtual void Component_PostDrawInv(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale) { }

        public virtual void Component_SetDefaults(Item item) { }

        public virtual void Component_LoadData(Item item, TagCompound tag) { }

        public virtual void Component_SaveData(Item item, TagCompound tag) { }

        public virtual void Component_UpdateInventory(Item item, Player player) { }

        public sealed override bool CanPickup(Item item, Player player)
            => Enabled ? Component_CanPickup(item, player) : base.CanPickup(item, player);

        public sealed override bool CanRightClick(Item item)
            => Enabled ? Component_CanRCLK(item) : base.CanRightClick(item);

        public sealed override bool OnPickup(Item item, Player player)
            => Enabled ? Component_OnPickup(player, item) : base.OnPickup(item, player);

        public sealed override bool PreDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            if (Enabled)
                return Component_PreDrawInv(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);

            else
                return base.PreDrawInInventory(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale);
        }

        public sealed override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (Enabled)
                return Component_PreDrawWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);

            else
                return base.PreDrawInWorld(item, spriteBatch, lightColor, alphaColor, ref rotation, ref scale, whoAmI);
        }

        public sealed override void RightClick(Item item, Player player)
        { if (Enabled) Component_OnRCLK(item, player); }

        public sealed override void OnConsumeItem(Item item, Player player)
        { if (Enabled) Component_OnConsumeItem(item, player); }

        public sealed override void PostDrawInInventory(Item item, SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        { if (Enabled) Component_PostDrawInv(item, spriteBatch, position, frame, drawColor, itemColor, origin, scale); }

        public sealed override void PostDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        { if (Enabled) Component_PostDrawWorld(item, spriteBatch, lightColor, alphaColor, rotation, scale, whoAmI); }

        public sealed override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        { if (Enabled) Component_ModifyTooltips(item, tooltips); }

        public sealed override void SetDefaults(Item entity)
        { if (Enabled) Component_SetDefaults(entity); }

        public sealed override void SaveData(Item item, TagCompound tag)
        { if (Enabled) Component_SaveData(item, tag); }

        public sealed override void LoadData(Item item, TagCompound tag)
        { if (Enabled) Component_LoadData(item, tag); }

        public sealed override void UpdateInventory(Item item, Player player)
        { if (Enabled) Component_UpdateInventory(item, player); }
    }
}
