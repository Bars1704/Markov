using System;
using System.Collections.Generic;

namespace MarkovTest.ThreeDimension.Sequences
{
    public interface ISequence<T> : ISequencePlayable<T> where T : IEquatable<T>
    {
        public bool CanPlay(MarkovSimulation<T> simulation);
        public void Reset();

        public void Init();
        public void OnPlay();
        public List<ISequencePlayable<T>> Playables { get; }
    }
}