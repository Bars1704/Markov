using MarkovTest.TwoDimension.Patterns;
using System;
using System.Linq;
using MarkovTest.Misc;

namespace MarkovTest.TwoDimension.Rules
{
    [Serializable]
    public class AllRule<T> : RuleBase<T> where T : IEquatable<T>
    {
        public override void Play(MarkovSimulationTwoDim<T> simulation, RandomFabric randomFabric)
        {
            var coords = MainPattern.GetAllCoords(simulation);

            foreach (var coord in coords)
            {
                ApplyRuleEvent(coord.coord, coord.RotationSettings);
                var stamp = MatrixFormatter<T>.Rotate(Stamp, coord.RotationSettings.RotationAngle);
                if (coord.RotationSettings.FlipX)
                    MatrixFormatter<T>.MirrorNonAllocX(stamp);
                if (coord.RotationSettings.FlipY)
                    MatrixFormatter<T>.MirrorNonAllocY(stamp);
                simulation.Replace(coord.coord, stamp);
            }
        }


        public AllRule(Pattern<T> pattern, T[,] stamp)
            : base(pattern, stamp)
        {
        }

        public AllRule() : base()
        {
        }
    }
}