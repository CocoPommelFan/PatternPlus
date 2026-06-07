namespace PatternPlus.Pattern.Core.Base
{
    public class Circle : Shape
    {
        public Circle(int tileCount, bool isHalf, float pseudoAngle, bool isInversed) : base(isHalf, pseudoAngle, isInversed)
        {
            Angles = PatternUtils.CalculateCircleAngles(tileCount: tileCount, isHalf: IsHalf);
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}
