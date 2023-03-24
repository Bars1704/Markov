using System;
using MarkovTest.Misc;

namespace MarkovTest.TwoDimension.Sequences
{
    public interface ISequencePlayable<T> where T : IEquatable<T>
    {
        public event Action OnPlayed;
        public void Play(MarkovSimulation<T> simulation, RandomFabric randomFabric);
    }
}