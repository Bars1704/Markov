using System;

namespace MarkovTest
{
    public interface IMarkovSimulation<T> where T : IEquatable<T>
    {
        void Play(int? seed = default);
    }
}