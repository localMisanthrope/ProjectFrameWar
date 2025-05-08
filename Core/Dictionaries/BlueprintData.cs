using ProjectFrameWar.Content.Items.Bases;
using System.Collections.Generic;

using static ProjectFrameWar.Core.Extensions.ItemExtensions;

namespace ProjectFrameWar.Core.Dictionaries
{
    internal class BlueprintData
    {
        public static Dictionary<string, BlueprintRecipe> blueprintRecipes = new()
        {
            {"Resource_NeuralSensor", new([
                GetItem<Resource>("Resource_AlloyPlate"), 
                GetItem<Resource>("Resource_Ferrite"), 
                GetItem<Resource>("Resource_Rubedo"), 
                GetItem<FramePart>("Neuroptics_Excalibur")],
                [10, 10, 10, 1], 5)},

            {"Weapon_Rifle_Latron", new([
                GetItem<Resource>("Resource_Morphics"),
                GetItem<Resource>("Resource_Plastids"),
                GetItem<Resource>("Resource_AlloyPlate"),
                GetItem<Resource>("Resource_Salvage")],
                [5, 800, 1000, 900], 1)},

            #region EXCALIBUR
            {"Chassis_Excalibur", new([
                GetItem<Resource>("Resource_Morphics"), 
                GetItem<Resource>("Resource_Ferrite"), 
                GetItem<Resource>("Resource_Rubedo")],
                [1, 1000, 300, 0], 0) },

            {"Neuroptics_Excalibur", new([
                GetItem<Resource>("Resource_AlloyPlate"), 
                GetItem<Resource>("Resource_NeuralSensor"), 
                GetItem<Resource>("Resource_PolymerBundle"), 
                GetItem<Resource>("Resource_Rubedo")],
                [150, 1, 150, 500], 0) },

            {"Systems_Excalibur", new([
                GetItem<Resource>("Resource_ControlModule"), 
                GetItem<Resource>("Resource_Morphics"), 
                GetItem<Resource>("Resource_Salvage"),
                GetItem<Resource>("Resource_Plastids")],
                [1, 1, 500, 220], 0) },

            {"Frame_Excalibur", new([
                GetItem<FramePart>("Neuroptics_Excalibur"),
                GetItem<FramePart>("Chassis_Excalibur"),
                GetItem<FramePart>("Systems_Excalibur"),
                GetItem<Resource>("Resource_OrokinCell")],
                [1, 1, 1, 1], 1)},
            #endregion

            #region FROST
            {"Chassis_Frost", blueprintRecipes["Chassis_Excalibur"]},
            {"Neuroptics_Frost", blueprintRecipes["Neuroptics_Excalibur"] },
            {"Systems_Frost", new([
                GetItem<Resource>("Resource_ControlModule"), 
                GetItem<Resource>("Resource_Morphics"), 
                GetItem<Resource>("Resource_Salvage"), 
                GetItem<Resource>("Resource_Plastids")],
                [1, 1, 500, 500], 0) },
            {"Frame_Frost", new([
                GetItem<FramePart>("Neuroptics_Frost"),
                GetItem<FramePart>("Chassis_Frost"),
                GetItem<FramePart>("Systems_Frost"),
                GetItem<Resource>("Resource_OrokinCell")],
                [1, 1, 1, 1], 1)},
            #endregion

            #region MAG
            {"Chassis_Mag", blueprintRecipes["Chassis_Excalibur"]},
            {"Neuroptics_Mag", blueprintRecipes["Neuroptics_Excalibur"]},
            {"Systems_Mag", blueprintRecipes["Systems_Excalibur"]},
            {"Frame_Mag", new([
                GetItem<FramePart>("Neuroptics_Mag"),
                GetItem<FramePart>("Chassis_Mag"),
                GetItem<FramePart>("Systems_Mag"),
                GetItem<Resource>("Resource_OrokinCell")],
                [1, 1, 1, 1], 1)},
            #endregion

            #region VOLT
            {"Chassis_Volt", blueprintRecipes["Chassis_Excalibur"]},
            {"Neuroptics_Volt", blueprintRecipes["Neuroptics_Excalibur"]},
            {"Systems_Volt", blueprintRecipes["Systems_Excalibur"]},
            {"Frame_Volt", new([
                GetItem<FramePart>("Neuroptics_Volt"), 
                GetItem<FramePart>("Chassis_Volt"), 
                GetItem<FramePart>("Systems_Volt"), 
                GetItem<Resource>("Resource_OrokinCell")], 
                [1, 1, 1, 1], 1)}
            #endregion
        };
    }
}