using System;
using MarkovTest.Misc;
using MarkovTest.Sequences;
using MarkovTest.ThreeDimension.Patterns;
using Newtonsoft.Json;

namespace MarkovTest.ThreeDimension.Rules
{
    public abstract class RuleBase<T> : ISequencePlayable<T,MarkovSimulation<T>>, IResizable where T : IEquatable<T>
    {
        [JsonProperty] public T[,,] Stamp { get; set; }

        [JsonProperty] public Pattern<T> MainPattern { get; private set; }
        public Vector3Int Size => MainPattern.Size;

        protected RuleBase(Pattern<T> mainPattern, T[,,] stamp)
        {
            Stamp = stamp;
            MainPattern = mainPattern;
        }

        protected RuleBase()
        {
            MainPattern = new Pattern<T>();
            Stamp = new T[1, 1, 1];
        }

        protected void ApplyRuleEvent(Vector3Int coord, PatternDeformation3D rotationSettings)
        {
            OnRuleApplied?.Invoke(coord, rotationSettings);
            OnPlayed?.Invoke();
        }

        public event Action<Vector3Int, PatternDeformation3D> OnRuleApplied;

        public event Action OnPlayed;
        public abstract void Play(MarkovSimulation<T> simulation, RandomFabric randomFabric);

        public void Resize(Vector3Int newSize)
        {
            Stamp = MatrixFormatter<T>.Resize(Stamp, newSize.X, newSize.Y, newSize.Z);
            MainPattern.Resize(newSize);
        }
    }
}