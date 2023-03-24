using System;
using MarkovTest.TwoDimension;
using Newtonsoft.Json;

namespace MarkovTest.ThreeDimension.Sequences
{
    [Serializable]
    public class CycleSequence<T> : SequenceBase<T> where T : IEquatable<T>
    {
        [JsonProperty] public int Cycles { get; set; }
        [JsonIgnore] public int Counter { get; private set; }

        public CycleSequence(int cycles)
        {
            Cycles = cycles;
        }

        public CycleSequence()
        {
        }

        public override bool CanPlay(MarkovSimulation<T> simulation) => Counter < Cycles;

        public override void Reset() => Counter = 0;
        public override void Init() => Counter = 0;
        public override void OnPlay() => Counter++;
    }
}