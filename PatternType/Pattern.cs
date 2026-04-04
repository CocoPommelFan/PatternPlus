using System;
using System.Linq;
using ADOFAI;
namespace PatternPlus.PatternType
{
    public static class Pattern
    {
        public static scrFloor FirstPatternFloor { get; private set; }       
        public static scrFloor LastPatternFloor { get; private set; }
        public static float[] Angles { get; private set; }
        public static bool IsPseudo { get; private set; }
        public enum PatternType
        {
            Circle, PseudoCircle, Test
        }
        public static void Create()
        {
            // получение вкладки вкладки
            LevelEvent levelEvent = EditorTabLib.CustomTabManager.GetEvent((LevelEventType)902);

            if (levelEvent == null)
            {
                Main.Logger.Log("LevelEvent is null");
                return;
            }

            PatternType patternType = (PatternType)levelEvent["patternType"];

            bool isHalf = (bool)levelEvent["isHalf"];
            int tileCount = (int)levelEvent["tileCount"];
            float pseudoAngle = Convert.ToSingle(levelEvent["pseudoAngle"]);

            switch (patternType)
            {
                case PatternType.Circle:
                    CreateCircle(isHalf, tileCount);
                    IsPseudo = false;
                    break;
                case PatternType.PseudoCircle:
                    CreatePseudoCircle(isHalf, tileCount, pseudoAngle);
                    IsPseudo = true;
                    break;
            }
        }

        private static void CreateCircle(bool isHalf, int tileCount)
        {
            float[] totalAngles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: isHalf);
            BuildPattern(totalAngles);
        }

        private static void CreatePseudoCircle(bool isHalf, int tileCount, float pseudoAngle)
        {
            float firstAngle = (isHalf ? 180f : 360f) / tileCount;
            float[] totalAngles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: isHalf);
            float[] totalAnglesWithPseudos = PatternUtils.CalculatePseudoEveryNBeat(totalAngles: totalAngles, pseudoAngle: pseudoAngle, step: firstAngle);
            BuildPattern(totalAnglesWithPseudos);
        }

        private static void BuildPattern(float[] totalAngles)
        {
            var editor = Patches.EditorInstance.instance;
            
            Angles = totalAngles;

            foreach (float angle in Angles)
            {
                editor.CreateFloorWithCharOrAngle(angle, 'a');
            }
            
            LastPatternFloor = editor.selectedFloors.FirstOrDefault();
            
            // +1 ПОТОМУ ЧТО В СПИСКЕ УГЛОВ ПЕРВАЯ ПЛИТКА ИМЕЕТ УГОЛ -999 И ОНА УЧИТЫВАЕТСЯ
            int targetSeqId = LastPatternFloor.seqID - totalAngles.Length + 1;
            
            FirstPatternFloor = editor.floors.FirstOrDefault(f => f.seqID == targetSeqId);
            
            Main.Logger.Log($"{FirstPatternFloor.seqID} | {FirstPatternFloor.floatDirection}");

            foreach (var item in editor.floors)
            {
                Main.Logger.Log($"{item.seqID} - {item.floatDirection}");
            }

            EventUtils.AddSetSpeedToPatternStartAndEnd();
        }
    }
}
