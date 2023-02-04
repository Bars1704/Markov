using MarkovTest.TwoDimension.Patterns;

namespace MarkovTest.TwoDimension.Rules
{
    [Serializable]
    public class AllRule<T> : RuleBase<T> where T : IEquatable<T>
    {
        public override void Play(MarkovSimulationTwoDim<T> simulation)
        {
            var coords =
                Patterns.SelectMany(x =>
                    simulation.GetPatternCoords(x.Value)
                        .Select(y => (coord: y, RotationSettings: x.Key)));

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


        public AllRule(Pattern<T> pattern, T[,] stamp, RotationSettingsFlags rotationSettings)
            : base(pattern, stamp, rotationSettings)
        {
        }
    }
}