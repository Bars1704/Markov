using Newtonsoft.Json;

namespace MarkovTest.TwoDimension.Sequences
{
    public interface ISequencePlayable<T> where T : IEquatable<T>
    {
        public event Action OnPlayed;
        public void Play(MarkovSimulationTwoDim<T> simulation);
    }
}