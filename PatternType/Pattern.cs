using ADOFAI;
namespace PatternPlus.PatternType
{
    public static class Pattern
    {
        public enum PatternType
        {
            Circle, PseudoCircle, Test
        }
        public static void Create()
        {
            LevelEvent levelEvent = EditorTabLib.CustomTabManager.GetEvent((LevelEventType)902);

            if (levelEvent == null)
            {
                Main.Logger.Log("levelEvent is null!!!");
                return;
            }

            PatternType patternType = (PatternType)levelEvent["patternType"];

            bool isHalf = (bool)levelEvent["isHalf"];
            int tileCount = (int)levelEvent["tileCount"];
            float pseudoAngle = (float)levelEvent["pseudoAngle"];

            Main.Logger.Log($"{patternType} | {isHalf} | {tileCount}");

            switch (patternType)
            {
                case PatternType.Circle:
                    CreateCircle(isHalf, tileCount);
                    break;
                case PatternType.PseudoCircle:
                    CreatePseudoCircle(isHalf, tileCount, pseudoAngle);
                    break;
            }
        }

        private static void CreateCircle(bool isHalf, int tileCount)
        {   
            if (!Patches.EditorInstance.instance.SelectionIsSingle())
            {
                return;
            }
            
            float[] totalAngles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: isHalf);

            foreach (float angle in totalAngles)
            {
                Main.Logger.Log($"{angle}");
                Patches.EditorInstance.instance.CreateFloorWithCharOrAngle(angle, 'a');
            }
        }

        private static void CreatePseudoCircle(bool isHalf, int tileCount, float pseudoAngle)
        {
            float firstAngle =  (isHalf ? 180f : 360f) / tileCount;

            float[] totalAngles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: isHalf);
            float[] totalAnglesWithPseudos = PatternUtils.CalculatePseudoEveryNBeat(totalAngles: totalAngles, pseudoAngle: pseudoAngle, step: firstAngle);

            foreach (float angle in totalAnglesWithPseudos)
            {
                Main.Logger.Log($"{angle}");
                Patches.EditorInstance.instance.CreateFloorWithCharOrAngle(angle, 'a');
            }

        }
    }
}
