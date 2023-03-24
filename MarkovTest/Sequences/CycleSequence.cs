using System;
using Newtonsoft.Json;

namespace MarkovTest.Sequences
{
    [Serializable]
    public class CycleSequence<TSimElement, TSimType> : SequenceBase<TSimElement, TSimType>
        where TSimElement : IEquatable<TSimElement> where TSimType : IMarkovSimulation<TSimElement>
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

        public override bool CanPlay(TSimType simulation) => Counter < Cycles;

        public override void Reset() => Counter = 0;
        public override void Init() => Counter = 0;
        public override void OnPlay() => Counter++;
    }
}