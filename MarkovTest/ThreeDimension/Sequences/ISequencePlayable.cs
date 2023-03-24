using MarkovTest.Misc;

namespace MarkovTest.ThreeDimension.Sequences
{
    public interface ISequencePlayable<T> where T : IEquatable<T>
    {
        public event Action OnPlayed;
        public void Play(MarkovSimulation<T> simulation, RandomFabric randomFabric);
    }
}