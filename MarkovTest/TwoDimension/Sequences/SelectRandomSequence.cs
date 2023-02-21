using System;
using System.Collections.Generic;
using System.Linq;
using MarkovTest.Misc;

namespace MarkovTest.TwoDimension.Sequences
{
    public class SelectRandomSequence<T> : SequenceBase<T> where T : IEquatable<T>
    {
        public override bool CanPlay(MarkovSimulationTwoDim<T> simulation) =>
            Playables.Cast<SequenceBase<T>>().Any(x => x.CanPlay(simulation));

        public override void Reset()
        {
        }

        public override void Init()
        {
        }

        public override void OnPlay()
        {
        }

        protected override void PlayOneShot(MarkovSimulationTwoDim<T> simulation, RandomFabric randomFabric)
        {
            OnPlay();
            Playables[randomFabric.Next.Next(0, Playables.Count)].Play(simulation, randomFabric);
            OnPlayablePlayed();
        }
    }
}