using MarkovTest.Misc;
using MarkovTest.ThreeDimension.Patterns;


namespace MarkovTest.ThreeDimension.Rules
{
    public class RandomRule<T> : RuleBase<T> where T : IEquatable<T>
    {
        public override void Play(MarkovSimulation<T> simulation, RandomFabric randomFabric)
        {
            var coords = MainPattern.GetAllCoords(simulation);

            var poolElem = simulation.CoordsPool.Get();
            var coordsList = poolElem.Value;

            coordsList.Clear();
            coordsList.AddRange(coords);

            if (!coordsList.Any())
                return;

            var currentCoordIndex = randomFabric.Next.Next(coordsList.Count);
            var currentCoord = coordsList[currentCoordIndex];

            ApplyRuleEvent(currentCoord.Item1, currentCoord.Item2);

            var coord = currentCoord.Item2;

            var stamp = MatrixFormatter<T>.RotateX(Stamp, coord.RotationAngleX);
            stamp = MatrixFormatter<T>.RotateY(stamp, coord.RotationAngleY);
            stamp = MatrixFormatter<T>.RotateZ(stamp, coord.RotationAngleZ);

            if (coord.FlipX)
                MatrixFormatter<T>.MirrorNonAllocX(stamp);
            if (coord.FlipY)
                MatrixFormatter<T>.MirrorNonAllocY(stamp);

            simulation.Replace(currentCoord.Item1, stamp);

            poolElem.Return();
        }

        public RandomRule(Pattern<T> pattern, T[,,] stamp)
            : base(pattern, stamp)
        {
        }

        public RandomRule() : base()
        {
        }
    }
}