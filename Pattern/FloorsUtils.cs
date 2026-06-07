using ADOFAI;
using HarmonyLib;
using PatternPlus.Pattern.Core.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using static PatternPlus.Pattern.Core.EventUtils;

namespace PatternPlus.Pattern.Core
{
    public static class FloorsUtils
    {
        private const float LEFT_DIRECTION = 180f;

        public enum TileDirection
        {
            Right = 0,
            Left = 180
        }

        public static TileDirection GetCurrentTileDirection()
        {
            var selectedFloor = Patches.EditorInstance.Instance?.selectedFloors?.FirstOrDefault();
            if (selectedFloor == null)
                Main.Logger.Log("No floor selected");

            return Math.Abs(selectedFloor.floatDirection) == LEFT_DIRECTION 
                ? TileDirection.Left
                : TileDirection.Right;
        }
        public static float GetFloorFloatDirectionByScrFloor(scrFloor floor)
        {
            return floor.floatDirection;
        }
        public static int GetFirstPatternFloorSeqID()
        {
            return ShapeController.CurrentShape.FirstFloor.seqID;

        }
        public static scrFloor GetFirstPatternFloor(scnEditor editor)
        {
            if (!editor.SelectionIsSingle() || editor.SelectionIsEmpty())
                return null;

            return editor.selectedFloors.FirstOrDefault();
        }

        public static float CalculateSetSpeedMultiplier(List<scrFloor> patternFloors)
        {
            float result = ShapeController.CurrentShape.IsPseudo ? 1f - 1f / (patternFloors.Count / 4f) + 1 : 1f - 1f / (patternFloors.Count / 2f);
            return result;
        }
        public static void AddSetSpeedToPatternStartAndEnd()
        {
            float startMultiplier = FloorsUtils.CalculateSetSpeedMultiplier(ShapeController.CurrentShape.Floors);
            float endMultiplier = 1 / startMultiplier;

            Main.Logger.Log($"MULTIPLIER: {startMultiplier}");

            // В НАЧАЛЕ ПАТТЕРНА
            // -1 ПОТОМУ ЧТО ПРИ ДОБАВЛЕНИИ ИВЕНТА НЕ УЧИТЫВАЕТСЯ ПЕРВАЯ ПЛИТКА В floors С -999 ГРАДУСОМ
            EventUtils.CreateSetSpeedEventWithMultiplier(ShapeController.CurrentShape.FirstFloor.seqID - 1, MultiplyType.Multiplier, startMultiplier);

            // В КОНЦЕ ПАТТЕРНА
            EventUtils.CreateSetSpeedEventWithMultiplier(ShapeController.CurrentShape.LastFloor.seqID, MultiplyType.Multiplier, endMultiplier);
        }

        public static void AddRadiusScaleToPattern()
        {


            if (LevelEventData.RadiusScale1 == 100 && LevelEventData.RadiusScale2 == 100)
                return;

            Main.Logger.Log($"{LevelEventData.RadiusScale1} - {LevelEventData.RadiusScale2}");

            for (int i = 0; i < ShapeController.CurrentShape.Floors.Count; i++)
            {
                if (i % 2 == 0)
                {
                    EventUtils.CreateRadiusScaleEventWithValue(ShapeController.CurrentShape.Floors[i].seqID, LevelEventData.RadiusScale1);
                }
                else
                {
                    EventUtils.CreateRadiusScaleEventWithValue(ShapeController.CurrentShape.Floors[i].seqID, LevelEventData.RadiusScale2);
                }
            }
            // финальный Radius Scale
            EventUtils.CreateRadiusScaleEventWithValue(ShapeController.CurrentShape.Floors.Last().seqID, 100f);
        }

    }
}
