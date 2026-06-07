using System;
using ADOFAI;

namespace PatternPlus.Pattern.Core.Base 
{
    public static class ShapeFactory
    {
        public static Shape ShapeBuild(PatternType type, int tileCount, bool isHalf, float pseudoAngle, bool isInversed)
        {
            return type switch
            {
                PatternType.Circle => new Circle(
                        tileCount: tileCount,
                        isHalf: isHalf,
                        pseudoAngle: pseudoAngle,
                        isInversed: isInversed),
                PatternType.PseudoCircle => new PseudoCircle(
                        tileCount: tileCount,
                        isHalf: isHalf,
                        pseudoAngle: pseudoAngle,
                        isInversed: isInversed),
                _ => throw new NotImplementedException()

            };
        }
    }
    public static class ShapeController
    {
        public static Shape CurrentShape { get; private set; }

        
        public static void Create()
        {
            CurrentShape = ShapeFactory.ShapeBuild(LevelEventData.PatternType, LevelEventData.TileCount, LevelEventData.IsHalf, LevelEventData.PseudoAngle, LevelEventData.IsInversed);

            CurrentShape.Build();
        }
    }
}
