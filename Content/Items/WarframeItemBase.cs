using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Players;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Content.Items
{
    [Autoload(false)]
    internal class WarframeItem_HeadBase(string frameName) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Texture => $"frame_{frameName}_Helmet";

        public override string Name => $"{frameName}Helmet";

        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;

            base.SetStaticDefaults();
        }

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.headSlot = EquipLoader.AddEquipTexture(Mod, $"{Texture}_Body", EquipType.Head);

            base.SetDefaults();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => 
            body == ItemExtensions.GetItem<WarframeItem_ChestBase>($"Frame_{frameName}_Chest") && 
            legs == ItemExtensions.GetItem<WarframeItem_LegsBase>($"Frame_{frameName}_Legs");

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue($"Mods.ProjectFrameWar.Warframes.SetBonus_{frameName}");

            player.TryGetComponent(out WarframeComponent frame);
            frame.currentFrameData = FrameLoader.allFrameData[frameName];

            base.UpdateArmorSet(player);
        }
    }

    [Autoload(false)]
    internal class WarframeItem_ChestBase(string frameName) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Texture => $"frame_{frameName}_Chest";

        public override string Name => $"{frameName}Chest";

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.bodySlot = EquipLoader.AddEquipTexture(Mod, $"{Texture}_Body", EquipType.Body);

            base.SetDefaults();
        }
    }

    [Autoload(false)]
    internal class WarframeItem_LegsBase(string frameName) : ModItem
    {
        protected override bool CloneNewInstances => true;

        public override string Texture => $"frame_{frameName}_Legs";

        public override string Name => $"{frameName}Legs";

        public override void SetDefaults()
        {
            Item.height = 20;
            Item.width = 20;

            Item.legSlot = EquipLoader.AddEquipTexture(Mod, $"{Texture}_Legs", EquipType.Legs);

            base.SetDefaults();
        }
    }
}