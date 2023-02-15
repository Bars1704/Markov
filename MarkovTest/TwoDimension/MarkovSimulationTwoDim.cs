using System.Runtime.Serialization;
using MarkovTest.Misc;
using MarkovTest.ObjectPool;
using MarkovTest.TwoDimension.Patterns;
using MarkovTest.TwoDimension.Rules;
using MarkovTest.TwoDimension.Sequences;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MarkovTest.TwoDimension
{
    [Serializable]
    public class MarkovSimulationTwoDim<T> : IResizable where T : IEquatable<T>
    {
        /// <summary>
        /// Invokes, when simulation changed
        /// </summary>
        public event Action<T[,]> OnSimulationChanged = _ => { };

        public event Action? OnPlayed;

        //TODO: Сделать пул пулов, подумать в принципе как убрать эту зависимость
        [JsonIgnore] public readonly ObjectPool<List<(Vector2Int, PatternDeformation)>> CoordsPool =
            new ObjectPool<List<(Vector2Int, PatternDeformation)>>();
        
        [JsonProperty] public List<ISequencePlayable<T>> Playables = new List<ISequencePlayable<T>>();

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
        public MarkovSimulationTwoDim(T[,] initialSimulationState)
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

        public MarkovSimulationTwoDim()
        {
            DefaultState = new T[0, 0];
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

        /// <summary>
        /// Returns coordinates, that matches to pattern
        /// </summary>
        /// <param name="pattern">Pattens, that will be checked</param>
        /// <returns> Coordinates, that matches to pattern</returns>
        public IEnumerable<Vector2Int> GetPatternCoords(Pattern<T> pattern)
        {
            for (var x = 0; x < Simulation.GetLength(0); x++)
            {
                for (var y = 0; y < Simulation.GetLength(1); y++)
                {
                    var coord = new Vector2Int(x, y);
                    if (pattern.Compare(this, coord))
                        yield return coord;
                }
            }
        }


        public void Play(MarkovSimulationTwoDim<T> simulation, int? seed = default)
        {
            var RandomFabric = seed.HasValue ? new RandomFabric(seed.Value) : new RandomFabric();

            Array.Copy(DefaultState, Simulation, DefaultState.Length);
            foreach (var playable in Playables)
            {
                Thread.Sleep(100);
                playable.Play(simulation,RandomFabric);
                OnPlayed?.Invoke();
            }
        }
    }
}