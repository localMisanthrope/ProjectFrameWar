using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Systems
{
    internal class OrokinReactorEvent : ModSystem
    {
        public bool isActive;
        public bool minibossActive;

        public Item[] insertedRelics;
        public List<Player> participants;

        public int closedFissureCount;

        public int degenFissureCount;

        public int reactantCount;
        public int reactantNeed;

        public const string LOCAL_KEY = "Mods.ProjectFrameWar.Events.VoidFissure";

        public void InitNew(Player player)
        {
            isActive = true;

            closedFissureCount = 0;
            degenFissureCount = 0;

            reactantCount = 0;
            reactantNeed = insertedRelics.Length * 25;

            participants ??= [player];

            if (!Main.dedServ)
                Main.NewText(Language.GetText($"{LOCAL_KEY}.Activation").Format(player.name));

            //To-do: Implement network-based localized text option.

            Mod.Logger.Debug("[DEBUG]: Initaited Void Fissure event.");
        }
    }

    public class FissureBossBar : ModBossBar
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
        }
    }
}