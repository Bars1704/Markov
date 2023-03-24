using System;
using MarkovTest.Misc;
using MarkovTest.ThreeDimension.Patterns;


namespace MarkovTest.ThreeDimension.Rules
{
    [Serializable]
    public class AllRule<T> : RuleBase<T> where T : IEquatable<T>
    {
        public override void Play(MarkovSimulation<T> simulation, RandomFabric randomFabric)
        {
            var coords = MainPattern.GetAllCoords(simulation);

            foreach (var coord in coords)
            {
                ApplyRuleEvent(coord.coord, coord.RotationSettings);
                var stamp = MatrixFormatter<T>.RotateX(Stamp, coord.RotationSettings.RotationAngleX);
                stamp = MatrixFormatter<T>.RotateY(stamp, coord.RotationSettings.RotationAngleY);
                stamp = MatrixFormatter<T>.RotateZ(stamp, coord.RotationSettings.RotationAngleZ);
                
                if (coord.RotationSettings.FlipX)
                    MatrixFormatter<T>.MirrorNonAllocX(stamp);
                if (coord.RotationSettings.FlipY)
                    MatrixFormatter<T>.MirrorNonAllocY(stamp);
                simulation.Replace(coord.coord, stamp);
            }
        }


        public AllRule(Pattern<T> pattern, T[,,] stamp)
            : base(pattern, stamp)
        {
        }

        public AllRule() : base()
        {
        }
    }
}