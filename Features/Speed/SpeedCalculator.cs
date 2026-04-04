using System;
using System.Collections.Generic;
using System.Linq;
using PatternPlus.Core;

namespace PatternPlus.Features.Speed
{
    public static class SpeedCalculator
    {
        private const float LEFT_DIRECTION = 180f;

        public enum TileDirection
        {
            Right = 0,
            Left = 180
        }

        public static TileDirection GetCurrentTileDirection()
        {
            var selectedFloor = Core.Patches.EditorInstance.instance?.selectedFloors?.FirstOrDefault();
            if (selectedFloor == null)
                Core.Main.Logger.Log("No floor selected");

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
            return Pattern.Pattern.FirstPatternFloor.seqID;

        }
        public static scrFloor GetFirstPatternFloor(scnEditor editor)
        {
            if (!editor.SelectionIsSingle() || editor.SelectionIsEmpty())
                return null;

            return editor.selectedFloors.FirstOrDefault();
        }

        public static float CalculateSetSpeedMultiplier(List<scrFloor> patternFloors)
        {
            // IF TILE == 180* THEN (ANGLE - 180) * 1

            int index = Pattern.Pattern.IsPseudo ? 1 : 0;

            bool isMirrored = Core.Patches.EditorInstance.instance.floors[patternFloors[0].seqID - 1].floatDirection == 180 ? true : false;

            float rawAngle = Math.Abs(patternFloors[index].floatDirection);
            float angle = isMirrored ? Math.Abs(rawAngle - 180) : rawAngle;
            float multiplier = Pattern.Pattern.IsPseudo ? 2f : 1f;
            float result = (1f - angle / 180f) * multiplier;

            return result;
        }
    }
}
