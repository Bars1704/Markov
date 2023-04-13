using System;
using System.Collections.Generic;
using Markov.MarkovTest.Misc;
using Newtonsoft.Json;

namespace Markov.MarkovTest.Sequences
{
    public abstract class SequenceBase<TSimElement, TSimType> : ISequence<TSimElement, TSimType>
        where TSimElement : IEquatable<TSimElement>
        where TSimType : IMarkovSimulation<TSimElement>
    {
        [JsonProperty] public List<ISequencePlayable<TSimElement, TSimType>> Playables { get; private set; }

        public event Action OnPlayed;

        public void Play(TSimType simulation, RandomFabric randomFabric)
        {
            Init();
            PlayOneShot(simulation, randomFabric);
            Reset();
        }

        protected virtual void PlayOneShot(TSimType simulation, RandomFabric randomFabric)
        {
            while (CanPlay(simulation))
            {
                OnPlay();
                Playables.ForEach(x => x.Play(simulation, randomFabric));
                OnPlayablePlayed();
            }
        }

        protected void OnPlayablePlayed()
        {
            OnPlayed?.Invoke();
        }

        protected SequenceBase()
        {
            Playables = new List<ISequencePlayable<TSimElement, TSimType>>();
        }

        public abstract bool CanPlay(TSimType simulation);

        public abstract void Reset();

        public abstract void OnPlay();
        public abstract void Init();
    }
}