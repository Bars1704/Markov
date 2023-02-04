namespace MarkovTest.TwoDimension.Sequences
{
    public interface ISequence<T> : ISequencePlayable<T> where T : IEquatable<T>
    {
        public bool CanPlay(MarkovSimulationTwoDim<T> simulation);
        public void Reset();

        public void Init();
        public void OnPlay();
        public List<ISequencePlayable<T>> Playables { get; }
    }
}