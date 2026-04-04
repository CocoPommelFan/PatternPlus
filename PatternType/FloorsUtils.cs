using System.Collections.Generic;
using System.Linq;

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

        public static float CalculateSetSpeedMultiplier(List<scrFloor> patternFloors)
        {
            int index = Pattern.IsPseudo ? 1 : 0;
            float angle = patternFloors[index].floatDirection;
            float multiplier = Pattern.IsPseudo ? 2f : 1f;
            float result = (1f - angle / 180f) * multiplier;

            Main.Logger.Log($"=== CalculateSetSpeedMultiplier ===");
            Main.Logger.Log($"IsPseudo: {Pattern.IsPseudo}");
            Main.Logger.Log($"Index: {index}");
            Main.Logger.Log($"Angle: {angle}");
            Main.Logger.Log($"Multiplier: {multiplier}");
            Main.Logger.Log($"Result: {result}");
            Main.Logger.Log($"PatternFloors count: {patternFloors.Count}");
            
            for (int i = 0; i < patternFloors.Count; i++)
            {
                Main.Logger.Log($"  [{i}] seqID: {patternFloors[i].seqID}, angle: {patternFloors[i].floatDirection}");
            }
            
            return result;
        }
    }
}
