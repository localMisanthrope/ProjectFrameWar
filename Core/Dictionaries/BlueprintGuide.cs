using ProjectFrameWar.Content.Items;
using System.Collections.Generic;
using Terraria;

using static ProjectFrameWar.Core.Extensions.ItemExtensions;

namespace ProjectFrameWar.Core.Dictionaries
{
    internal class BlueprintGuide
    {
        public static Dictionary<string, (Item[] ingredients, int[] requirements, int resultAmount)> blueprintRecipes = new()
        {
            {"Resource_NeuralSensor", ([GetItem<Resource>("Resource_AlloyPlate"), GetItem<Resource>("Resource_Ferrite"), GetItem<Resource>("Resource_Rubedo"), GetItem<FramePart>("Neuroptics_Excalibur")],
                [10, 10, 10, 1], 5)},

            {"FirstEra_Chassis", (([],
                [], 0)) },

            {"FirstEra_Neuroptics", (([GetItem<Resource>("Resource_AlloyPlate"), GetItem<Resource>("Resource_NeuralSensor"), GetItem<Resource>("Resource_PolymerBundle"), GetItem<Resource>("Resource_Rubedo")],
                [150, 1, 150, 500], 0)) },

            {"FirstEra_Systems", (([],
                [], 0)) }
        };
    }
}