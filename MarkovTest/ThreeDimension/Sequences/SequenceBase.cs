using MarkovTest.Misc;
using Newtonsoft.Json;

namespace MarkovTest.ThreeDimension.Sequences
{
    public abstract class SequenceBase<T> : ISequence<T> where T : IEquatable<T>
    {
        [JsonProperty] public List<ISequencePlayable<T>> Playables { get; private set; }

        public event Action OnPlayed;

        public void Play(MarkovSimulation<T> simulation, RandomFabric randomFabric)
        {
            Init();
            PlayOneShot(simulation, randomFabric);
            Reset();
        }

        protected virtual void PlayOneShot(MarkovSimulation<T> simulation, RandomFabric randomFabric)
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
            Playables = new List<ISequencePlayable<T>>();
        }

        public abstract bool CanPlay(MarkovSimulation<T> simulation);

        public abstract void Reset();

        public abstract void OnPlay();
        public abstract void Init();
    }
}