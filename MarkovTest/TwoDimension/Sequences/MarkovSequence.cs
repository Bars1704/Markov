using MarkovTest.TwoDimension.Rules;
using Newtonsoft.Json;

namespace MarkovTest.TwoDimension.Sequences
{
    public class MarkovSequence<T> : SequenceBase<T> where T : IEquatable<T>
    {
        private bool _onRuleApplied { get; set; }
        private bool _firstPlay { get; set; } = true;

        private void OnRuleApplied(Vector2Int vector2Int, PatternDeformation deformation)
        {
            _onRuleApplied = true;
        }

        public override bool CanPlay(MarkovSimulationTwoDim<T> simulation)
        {
            var result = _onRuleApplied || _firstPlay;
            _firstPlay = false;
            return result;
        }

        private void UnsubRecursive(ISequence<T> sequence)
        {
            foreach (var x in sequence.Playables.OfType<RuleBase<T>>())
                x.OnRuleApplied -= OnRuleApplied;
            foreach (var x in sequence.Playables.OfType<SequenceBase<T>>())
                UnsubRecursive(x);
        }

        private void SubRecursive(ISequence<T> sequence)
        {
            foreach (var x in sequence.Playables.OfType<RuleBase<T>>())
                x.OnRuleApplied += OnRuleApplied;
            foreach (var x in sequence.Playables.OfType<SequenceBase<T>>())
                SubRecursive(x);
        }

        public override void Reset()
        {
            UnsubRecursive(this);
            _firstPlay = true;
            _onRuleApplied = false;
        }

        public override void Init()
        {
            foreach (var x in Playables.OfType<RuleBase<T>>())
                x.OnRuleApplied += OnRuleApplied;

            SubRecursive(this);
            _onRuleApplied = false;
        }

        public override void OnPlay()
        {
            _onRuleApplied = false;
        }
    }
}