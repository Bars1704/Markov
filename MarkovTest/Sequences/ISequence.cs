namespace MarkovTest.Sequences
{
    public interface ISequence<TSimElement, TSimType> : ISequencePlayable<TSimElement, TSimType>
        where TSimElement : IEquatable<TSimElement> where TSimType : IMarkovSimulation<TSimElement>
    {
        public bool CanPlay(TSimType simulation);
        public void Reset();

        public void Init();
        public void OnPlay();
        public List<ISequencePlayable<TSimElement, TSimType>> Playables { get; }
    }
}