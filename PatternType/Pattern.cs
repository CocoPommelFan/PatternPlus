using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using ADOFAI;
namespace PatternPlus.PatternType
{
    public static class Pattern
    {
        public static List<scrFloor> PatternFloors { get; private set; }
        public static scrFloor FirstPatternFloor { get; private set; }       
        public static scrFloor LastPatternFloor { get; private set; }
        public static float[] Angles { get; private set; }
        public static bool IsPseudo { get; private set; }
        public static float PseudoAngle { get; private set; }
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
            bool isInversed = (bool)levelEvent["isInversed"];
            int tileCount = (int)levelEvent["tileCount"];
            float pseudoAngle = Convert.ToSingle(levelEvent["pseudoAngle"]);
            switch (patternType)
            {
                case PatternType.Circle:
                    IsPseudo = false;
                    CreateCircle(isHalf, tileCount, isInversed);
                    break;
                case PatternType.PseudoCircle:
                    IsPseudo = true;
                    CreatePseudoCircle(isHalf, tileCount, pseudoAngle, isInversed);
                    break;
            }
        }

        private static void CreateCircle(bool isHalf, int tileCount, bool isInversed)
        {
            float[] totalAngles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: isHalf);
            BuildPattern(totalAngles, isInversed);
        }

        private static void CreatePseudoCircle(bool isHalf, int tileCount, float pseudoAngle, bool isInversed)
        {
            float firstAngle = (isHalf ? 180f : 360f) / tileCount;
            float[] totalAngles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: isHalf);
            float[] totalAnglesWithPseudos = PatternUtils.CalculatePseudoEveryNBeat(totalAngles: totalAngles, pseudoAngle: pseudoAngle, step: firstAngle);
            BuildPattern(totalAnglesWithPseudos, isInversed);
        }

        private static void BuildPattern(float[] totalAngles, bool isInversed)
        {
            var editor = Patches.EditorInstance.instance;

            Angles = totalAngles;

            if (isInversed)
            {
                for (int i = 0; i < totalAngles.Length; i++)
                {
                    totalAngles[i] *= -1;
                }
            }

            foreach (float angle in Angles)
            {
                editor.CreateFloorWithCharOrAngle(angle, 'a');
            }
            
            LastPatternFloor = editor.selectedFloors.FirstOrDefault();
            // +1 ПОТОМУ ЧТО В СПИСКЕ УГЛОВ ПЕРВАЯ ПЛИТКА ИМЕЕТ УГОЛ -999 И ОНА УЧИТЫВАЕТСЯ
            int targetSeqId = LastPatternFloor.seqID - totalAngles.Length + 1;
            
            FirstPatternFloor = editor.floors.FirstOrDefault(f => f.seqID == targetSeqId);

            PatternFloors = editor.floors
                .Where(f => f.seqID >= FirstPatternFloor.seqID && f.seqID <= LastPatternFloor.seqID)
                .ToList();

            // ДОП ПЛИТКА ЧТОБЫ НЕ ОБОСРАТЬСЯ
            editor.CreateFloorWithCharOrAngle(Angles.Last(), 'a');

            EventUtils.AddSetSpeedToPatternStartAndEnd();
            EventUtils.AddRadiusScaleToWholePattern();
        }
    }
}
