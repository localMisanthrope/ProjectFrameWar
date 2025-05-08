using Terraria;

namespace ProjectFrameWar.Core
{
    internal record struct BlueprintRecipe(Item[] ingredients, int[] requirements, int resultAmount)
    {
        public Item[] ingredients = ingredients;
        public int[] requirements = requirements;
        public int resultAmount = resultAmount;
    }
}