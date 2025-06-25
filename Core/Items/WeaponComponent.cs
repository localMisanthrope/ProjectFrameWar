using ProjectFrameWar.Content.Items.Bases;
using ProjectFrameWar.Core.Extensions;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Items
{
    internal enum WeaponClass
    {
        Rifle,
        Shotgun,
        Sniper,
        Bow,
        Pistol,
        Melee
    }

    internal class WeaponComponent : ItemComponent
    {
        internal WeaponData data;

        internal WeaponClass type;

        public override void Component_SetDefaults(Item item)
        {
            item.damage = data.Damage;
        }

        public override void Component_ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            tooltips.Add(new(Mod, "TypeLine", Language.GetText($"{LOCAL_KEY}.WeaponType.{type}").Value));

            tooltips.Add(new(Mod, "CritChanceLine", "\n" + Language.GetText($"{LOCAL_KEY}.CriticalChance").Format(data.CriticalChance.FormatAsPercent(2))));
            tooltips.Add(new(Mod, "CritDamageLine", Language.GetText($"{LOCAL_KEY}.CriticalDamage").Format(data.CriticalDamage)));
            tooltips.Add(new(Mod, "StatusChanceLine", Language.GetText($"{LOCAL_KEY}.StatusChance").Format(data.StatusChance.FormatAsPercent(2))));
        }

        public override void Component_UpdateInventory(Item item, Player player)
        {
            if (!item.GetAllComponents().Any(x => x.Name == $"{data.Name}Component"))
                return;

            if ((player.HeldItem != item || Main.mouseItem != item) && item.GetAllComponents().First(x => x.Name == $"{data.Name}Component").Enabled)
                item.TryDisableComponent(data.Name);

            if ((player.HeldItem == item || Main.mouseItem == item) && !item.GetAllComponents().First(x => x.Name == $"{data.Name}Component").Enabled)
                item.TryEnableComponent(data.Name);
        }
    }
}