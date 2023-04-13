using System;

namespace Markov.MarkovTest
{
    public interface IMarkovSimulation<T> where T : IEquatable<T>
    {
        void Play(int? seed = default);
    }
}