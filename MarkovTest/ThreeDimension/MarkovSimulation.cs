using System.Runtime.Serialization;
using MarkovTest.Misc;
using MarkovTest.ObjectPool;
using MarkovTest.ThreeDimension.Rules;
using Newtonsoft.Json;
using MarkovTest.ThreeDimension.Sequences;

namespace MarkovTest.ThreeDimension
{
    [Serializable]
    public class MarkovSimulation<T> : IResizable, IMarkovSimulation where T : IEquatable<T>
    {
        /// <summary>
        /// Invokes, when simulation changed
        /// </summary>
        public event Action<T[,,]> OnSimulationChanged = _ => { };

        [JsonIgnore] public readonly ObjectPool<List<(Vector3Int, PatternDeformation3D)>> CoordsPool =
            new ObjectPool<List<(Vector3Int, PatternDeformation3D)>>();

        [JsonProperty] public List<ISequencePlayable<T>> Playables = new List<ISequencePlayable<T>>();

        /// <summary>
        /// Represents current simulation state
        /// </summary>
        [JsonIgnore]
        public T[,,] Simulation { get; private set; }

        /// <summary>
        /// Represents current simulation state
        /// </summary>
        [JsonProperty] public T[,,] DefaultState;

        /// <summary>
        /// Size of the simulation
        /// </summary>
        public Vector3Int Size =>
            new Vector3Int(DefaultState.GetLength(0), DefaultState.GetLength(1), DefaultState.GetLength(2));

        /// <summary>
        /// Value of the current point inside simulation
        /// </summary>
        /// <param name="x">X coord inside simulation</param>
        /// <param name="y">X coord inside simulation</param>
        public T this[int x, int y, int z] => Simulation[x, y, z];

        /// <summary>
        /// Value of the current point inside simulation
        /// </summary>
        /// <param name="coords">coords inside simulation</param>
        public T this[Vector3Int coords] => this[coords.X, coords.Y, coords.Z];

        /// <summary>
        /// </summary>
        /// <param name="initialSimulationState">State, that simulation will have on start</param>
        /// <param name="seed">
        /// Seed for pseudo-random inside simulation.
        /// You can set same seed to get same results every time, or set seed to null, to get random seed
        /// </param>
        public MarkovSimulation(T[,,] initialSimulationState)
        {
            DefaultState = initialSimulationState;
            InitDefaultState();
        }

        private void InitDefaultState()
        {
            Simulation = new T[DefaultState.GetLength(0), DefaultState.GetLength(1), DefaultState.GetLength(2)];
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            InitDefaultState();
        }

        public void Resize(Vector3Int newSize)
        {
            DefaultState = MatrixFormatter<T>.Resize(DefaultState, newSize.X, newSize.Y, newSize.Z);
            InitDefaultState();
        }

        public MarkovSimulation()
        {
            DefaultState = new T[0, 0, 0];
        }

        /// <summary>
        /// Replaces values in simulation with values from stamp
        /// </summary>
        /// <param name="coord">Coordinates of up left corner from place ,where stamp will placed</param>
        /// <param name="stamp">Values, that will be set in simulation</param>
        public void Replace(Vector3Int coord, T[,,] stamp)
        {
            for (var x = 0; x < stamp.GetLength(0); x++)
            for (var y = 0; y < stamp.GetLength(1); y++)
            for (var z = 0; z < stamp.GetLength(2); z++)
                if (!stamp[x, y, z].Equals(default(T)))
                    Simulation[coord.X + x, coord.Y + y, coord.Z + z] = stamp[x, y, z];

            OnSimulationChanged.Invoke(Simulation);
        }


        public void Play(int? seed = default)
        {
            var RandomFabric = seed.HasValue ? new RandomFabric(seed.Value) : new RandomFabric();

            Array.Copy(DefaultState, Simulation, DefaultState.Length);
            foreach (var playable in Playables)
            {
                playable.Play(this, RandomFabric);
            }
        }
    }
}