using System.Linq;
using ADOFAI;

namespace PatternPlus.PatternType
{
    public static class PatternUtils
    {
        public static float[] CalculateCircleAngles(int tileCount, bool isHalf)
        {   
            float firstAngle =  (isHalf ? 180f : 360f) / tileCount;
            float[] totalAngles = new float[tileCount];
            float step = firstAngle;
            if (Patches.EditorInstance.instance.selectedFloors[0].floatDirection == 180f) 
            {
                firstAngle = 180 - firstAngle;

                for (int i = 0; i < tileCount; i++)
                {
                    totalAngles[i] = firstAngle;
                    firstAngle -= step;

                    if (UnityEngine.Mathf.Approximately(firstAngle, 0f))
                        firstAngle = 360;
                }
            }
            else
            {
                for (int i = 0; i < tileCount; i++)
                {
                    totalAngles[i] = firstAngle;
                    firstAngle += step;
                }
            }
            return totalAngles;
        }

        public static float[] CalculatePseudoEveryNBeat(float[] totalAngles, float step, int beatCount = 0, float pseudoAngle = 30)
        { 
            float[] anglesWithPseudo = new float[totalAngles.Length * 2];
            float firstPseudoAngle;
            float[] pseudoAngles = null;

            // если дорога идёт налево, т.е. угол развёртнутой плитки 180*. значит первая псевда это 30* -> 30 - 45 -> 30 - 90 ...
            // если дорога идёт направо, т.е. угол развёртнутой плитки 0*. значит первая псевда это 150* -> 150 + 45 -> 150 + 90 ...
            if (Patches.EditorInstance.instance.selectedFloors[0].floatDirection == 180f)
            {
                firstPseudoAngle = 0 + pseudoAngle;
                pseudoAngles = Enumerable.Range(0, totalAngles.Length)
                    .Select(i => firstPseudoAngle - step * i)
                    .ToArray();
            }
            else
            {
                firstPseudoAngle = 180 - pseudoAngle;

                pseudoAngles = Enumerable.Range(0, totalAngles.Length)
                    .Select(i => firstPseudoAngle + step * i)
                    .ToArray();
            }

            float[] result = Enumerable.Range(0, pseudoAngles.Length + totalAngles.Length)
                .Select(i => i % 2 == 0 ? pseudoAngles[i / 2] : totalAngles[i / 2])
                .ToArray();
                
            return result;
        }
    }
}