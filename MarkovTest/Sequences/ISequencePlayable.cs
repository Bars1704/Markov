using System;
using Markov.MarkovTest.Misc;

namespace Markov.MarkovTest.Sequences
{
    public interface ISequencePlayable<TSimElement, in TSimType>
        where TSimElement : IEquatable<TSimElement>
        where TSimType : IMarkovSimulation<TSimElement>
    {
        public event Action OnPlayed;
        public void Play(TSimType simulation, RandomFabric randomFabric);
    }
}