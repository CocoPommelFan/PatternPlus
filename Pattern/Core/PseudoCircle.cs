namespace PatternPlus.Pattern.Core.Base
{
    public class PseudoCircle : Shape
    {
        public override bool IsPseudo => true;
        public PseudoCircle(int tileCount, bool isHalf, float pseudoAngle, bool isInversed) : base(isHalf, pseudoAngle, isInversed)
        {
            float firstAngle = (IsHalf ? 180f : 360f) / tileCount;
            float[] totalAngles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: IsHalf);
            Angles = PatternUtils.CalculatePseudoEveryNBeat(totalAngles: totalAngles, pseudoAngle: pseudoAngle, step: firstAngle);
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}
