using System;
using System.Linq;
using MarkovTest.Misc;

namespace MarkovTest.Sequences
{
    public class SelectRandomSequence<TSimElement, TSimType> : SequenceBase<TSimElement, TSimType>
        where TSimElement : IEquatable<TSimElement> where TSimType : IMarkovSimulation<TSimElement>
    {
        public override bool CanPlay(TSimType simulation) =>
            Playables.Cast<SequenceBase<TSimElement, TSimType>>().Any(x => x.CanPlay(simulation));

        public override void Reset()
        {
        }

        public override void Init()
        {
        }

        public override void OnPlay()
        {
        }

        protected override void PlayOneShot(TSimType simulation, RandomFabric randomFabric)
        {
            OnPlay();
            Playables[randomFabric.Next.Next(0, Playables.Count)].Play(simulation, randomFabric);
            OnPlayablePlayed();
        }
    }
}