using MarkovTest.Misc;

namespace MarkovTest.Sequences
{
    public interface ISequencePlayable<TSimElement, TSimType>
        where TSimElement : IEquatable<TSimElement>
        where TSimType : IMarkovSimulation<TSimElement>
    {
        public event Action OnPlayed;
        public void Play(TSimType simulation, RandomFabric randomFabric);
    }
}