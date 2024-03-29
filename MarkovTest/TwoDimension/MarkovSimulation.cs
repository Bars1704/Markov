using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using Markov.MarkovTest.Misc;
using Markov.MarkovTest.ObjectPool;
using Markov.MarkovTest.Sequences;
using Markov.MarkovTest.TwoDimension.Rules;
using Newtonsoft.Json;

namespace Markov.MarkovTest.TwoDimension
{
    [Serializable]
    public class MarkovSimulation<T> : IResizable, IMarkovSimulation<T> where T : IEquatable<T>
    {
        /// <summary>
        /// Invokes, when simulation changed
        /// </summary>
        public event Action<T[,]> OnSimulationChanged = _ => { };

        [JsonIgnore] public readonly ObjectPool<List<(Vector2Int, PatternDeformation2D)>> CoordsPool =
            new ObjectPool<List<(Vector2Int, PatternDeformation2D)>>();

        [JsonProperty]
        public List<ISequencePlayable<T, MarkovSimulation<T>>> Playables =
            new List<ISequencePlayable<T, MarkovSimulation<T>>>();

        /// <summary>
        /// Represents current simulation state
        /// </summary>
        [JsonIgnore]
        public T[,] Simulation { get; private set; }

        /// <summary>
        /// Represents current simulation state
        /// </summary>
        [JsonProperty] public T[,] DefaultState;

        /// <summary>
        /// Size of the simulation
        /// </summary>
        public Vector2Int Size => new Vector2Int(DefaultState.GetLength(0), DefaultState.GetLength(1));

        /// <summary>
        /// Value of the current point inside simulation
        /// </summary>
        /// <param name="x">X coord inside simulation</param>
        /// <param name="y">X coord inside simulation</param>
        public T this[int x, int y] => Simulation[x, y];

        /// <summary>
        /// Value of the current point inside simulation
        /// </summary>
        /// <param name="coords">coords inside simulation</param>
        public T this[Vector2Int coords] => Simulation[coords.X, coords.Y];

        /// <summary>
        /// </summary>
        /// <param name="initialSimulationState">State, that simulation will have on start</param>
        /// <param name="seed">
        /// Seed for pseudo-random inside simulation.
        /// You can set same seed to get same results every time, or set seed to null, to get random seed
        /// </param>
        public MarkovSimulation(T[,] initialSimulationState)
        {
            DefaultState = initialSimulationState;
            Simulation = new T[DefaultState.GetLength(0), DefaultState.GetLength(1)];
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            Simulation = new T[DefaultState.GetLength(0), DefaultState.GetLength(1)];
        }

        public void Resize(Vector2Int newSize)
        {
            DefaultState = MatrixFormatter<T>.Resize(DefaultState, newSize.X, newSize.Y);
            Simulation = new T[DefaultState.GetLength(0), DefaultState.GetLength(1)];
        }

        public MarkovSimulation()
        {
            DefaultState = new T[1, 1];
        }

        /// <summary>
        /// Replaces values in simulation with values from stamp
        /// </summary>
        /// <param name="coord">Coordinates of up left corner from place ,where stamp will placed</param>
        /// <param name="stamp">Values, that will be set in simulation</param>
        public void Replace(Vector2Int coord, T[,] stamp)
        {
            for (var x = 0; x < stamp.GetLength(0); x++)
            for (var y = 0; y < stamp.GetLength(1); y++)
                if (!stamp[x, y].Equals(default(T)))
                    Simulation[coord.X + x, coord.Y + y] = stamp[x, y];

            OnSimulationChanged.Invoke(Simulation);
        }


        public void Play(int? seed = default)
        {
            var RandomFabric = seed.HasValue ? new RandomFabric(seed.Value) : new RandomFabric();

            Array.Copy(DefaultState, Simulation, DefaultState.Length);
            foreach (var playable in Playables)
            {
                Thread.Sleep(100);
                playable.Play(this, RandomFabric);
            }
        }
    }
}