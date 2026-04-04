using System;
using System.Linq;
using Discord;
using UnityEngine.UIElements;

namespace PatternPlus.PatternType
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
            var selectedFloor = Patches.EditorInstance.instance?.selectedFloors?.FirstOrDefault();
            if (selectedFloor == null)
                Main.Logger.Log("No floor selected");

            return selectedFloor.floatDirection == LEFT_DIRECTION 
                ? TileDirection.Left 
                : TileDirection.Right;
        }
        public static float GetFloorFloatDirectionByScrFloor(scrFloor floor)
        {
            return floor.floatDirection;
        }
        public static int GetFirstPatternFloorSeqID()
        {
            return Pattern.FirstPatternFloor.seqID;

        }
        public static scrFloor GetFirstPatternFloor(scnEditor editor)
        {
            if (!editor.SelectionIsSingle() || editor.SelectionIsEmpty())
                return null;

            return editor.selectedFloors.FirstOrDefault();
        }

        public static float CalculateSetSpeedMultiplier(scrFloor firstPatternFloor)
        {
            Main.Logger.Log($"{GetFloorFloatDirectionByScrFloor(firstPatternFloor)}");
            return 1f - (GetFloorFloatDirectionByScrFloor(firstPatternFloor) / 180f);
        }
    }
}
