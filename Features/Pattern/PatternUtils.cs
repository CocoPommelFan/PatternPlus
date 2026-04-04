using System;
using System.Linq;
using PatternPlus.Features.Speed;

namespace PatternPlus.Features.Pattern
{
    public static class PatternUtils
    {
        private const float FULL_CIRCLE = 360f;
        private const float HALF_CIRCLE = 180f;
        private const float ZERO_ANGLE = 0f;

        public static float[] CalculateCircleAngles(int tileCount, bool isHalf)
        {
            float firstAngle = (isHalf ? HALF_CIRCLE : FULL_CIRCLE) / tileCount;
            float[] totalAngles = new float[tileCount];
            float step = firstAngle;

            SpeedCalculator.TileDirection direction = SpeedCalculator.GetCurrentTileDirection();

            if (direction == SpeedCalculator.TileDirection.Left)
            {
                CalculateLeftDirectionAngles(totalAngles, ref firstAngle, step, tileCount);
            }
            else
            {
                CalculateRightDirectionAngles(totalAngles, ref firstAngle, step, tileCount);
            }

            return totalAngles;
        }

        public static float[] CalculatePseudoEveryNBeat(float[] totalAngles, float step, int beatCount = 0, float pseudoAngle = 30)
        {
            if (totalAngles == null || totalAngles.Length == 0)
                throw new ArgumentException("Total angles cannot be null or empty", nameof(totalAngles));

            SpeedCalculator.TileDirection direction = SpeedCalculator.GetCurrentTileDirection();

            float[] pseudoAngles = CalculatePseudoAngles(totalAngles.Length, step, pseudoAngle, direction);

            float[] result = Enumerable.Range(0, pseudoAngles.Length + totalAngles.Length)
                .Select(i => i % 2 == 0 ? pseudoAngles[i / 2] : totalAngles[i / 2])
                .ToArray();

            return result;
        }

        private static void CalculateLeftDirectionAngles(float[] totalAngles, ref float firstAngle, float step, int tileCount)
        {
            firstAngle = HALF_CIRCLE - firstAngle;

            for (int i = 0; i < tileCount; i++)
            {
                totalAngles[i] = firstAngle;
                firstAngle -= step;

                if (UnityEngine.Mathf.Approximately(firstAngle, ZERO_ANGLE))
                    firstAngle = FULL_CIRCLE;
            }
        }

        private static void CalculateRightDirectionAngles(float[] totalAngles, ref float firstAngle, float step, int tileCount)
        {
            for (int i = 0; i < tileCount; i++)
            {
                totalAngles[i] = firstAngle;
                firstAngle += step;
            }
        }

        private static float[] CalculatePseudoAngles(int count, float step, float pseudoAngle, SpeedCalculator.TileDirection direction)
        {
            float firstPseudoAngle;

            if (direction == SpeedCalculator.TileDirection.Left)
            {
                firstPseudoAngle = ZERO_ANGLE + pseudoAngle;
                return Enumerable.Range(0, count)
                    .Select(i => firstPseudoAngle - step * i)
                    .ToArray();
            }
            else
            {
                firstPseudoAngle = HALF_CIRCLE - pseudoAngle;
                return Enumerable.Range(0, count)
                    .Select(i => firstPseudoAngle + step * i)
                    .ToArray();
            }
        }
    }
}
