using System;

namespace MarkovTest.Misc
{
    /// <summary>
    /// Represents a pseudo-random <see cref="Random"/> instances generator, which is an algorithm that produces
    /// a sequence of numbers that meet certain statistical requirements for randomness.
    /// </summary>
    public class RandomFabric
    {
        /// <summary>
        /// This Random uses to generate different Randoms using seeds
        /// </summary>
        private readonly Random _seedGeneratorRandom;

        /// <summary>
        /// Returns <see cref="Random"/> object
        /// </summary>
        public Random Next => new Random(_seedGeneratorRandom.Next());

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomFabric"/> class using a default seed value
        /// </summary>
        public RandomFabric()
        {
            _seedGeneratorRandom = new Random();
        }

        /// <summary>
        /// Initializes a new instance of the  <see cref="RandomFabric"/> class, using the specified seed value.
        /// </summary>
        /// <param name="seed"> Seed for <see cref="Random"/> , that will generate next Random`s seeds </param>
        public RandomFabric(int seed)
        {
            _seedGeneratorRandom = new Random(seed);
        }
    }
}