using ProjectFrameWar.Core.Extensions;
using ProjectFrameWar.Core.Items;
using System.Linq;
using Terraria.ModLoader;

namespace ProjectFrameWar.Core.Players
{
    internal enum InfectionSeverity
    {
        Low,
        Severe,
        Critical
    }

    internal enum InfectionStatus
    {
        Pure,
        Planted,
        Budding,
        Blooming,
        Flowered
    }

    internal class InfectionManager : ModPlayer
    {
        public int infectionValue;

        public int infectionTimer;

        public InfectionSeverity severity;

        public InfectionStatus status;

        internal const int MAX_ITEM_COUNT = 10000;

        internal const int MAX_INFECTION_TIME = 1200;

        internal const int MAX_INFECTION_VALUE = 100;

        public float GetInfectionRate()
        {
            int count = 0;

            foreach (var item in Player.inventory.Where(x => x.HasComponent<InfectionComponent>()))
                count += item.stack;

            

            return count / MAX_ITEM_COUNT;
        }

        public override void PreUpdateBuffs()
        {
            if (GetInfectionRate() > 0f)
            {
                infectionTimer--;

                if (infectionTimer <= 0)
                {
                    infectionValue++;
                    infectionTimer = (int)(MAX_INFECTION_TIME / GetInfectionRate());
                }
            }

            //Upon reaching a specific status, a mutation will be added.
            //Mutations can be cleared once cured of the infection.
            //If cured, cannot be infected again for in-game 5 days.

            base.PreUpdateBuffs();
        }
    }
}