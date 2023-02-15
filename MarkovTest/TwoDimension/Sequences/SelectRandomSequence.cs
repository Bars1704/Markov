using System;
using System.Collections.Generic;
using MarkovTest.Misc;

namespace MarkovTest.TwoDimension.Sequences
{
    public class SelectRandomSequence<T> : SequenceBase<T> where T : IEquatable<T>
    {
        private bool _isPlayed = false;
        public override bool CanPlay(MarkovSimulationTwoDim<T> simulation) => !_isPlayed;

        public override void Reset()
        {
            _isPlayed = false;
        }

        public override void Init()
        {
            _isPlayed = false;
        }

        public override void OnPlay()
        {
            _isPlayed = true;
        }

        protected override void PlayOneShot(MarkovSimulationTwoDim<T> simulation, RandomFabric randomFabric)
        {
            OnPlay();
            Playables[randomFabric.Next.Next(0, Playables.Count)].Play(simulation, randomFabric);
            OnPlayablePlayed();
        }
    }
}