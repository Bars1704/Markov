using System;
using System.Linq;
using MarkovTest.TwoDimension;
using MarkovTest.TwoDimension.Rules;

namespace MarkovTest.Sequences
{
    public class MarkovSequence<TSimElement, TSimType> : SequenceBase<TSimElement, TSimType>
        where TSimElement : IEquatable<TSimElement> where TSimType : IMarkovSimulation<TSimElement>
    {
        private bool _onRuleApplied { get; set; }
        private bool _firstPlay { get; set; } = true;

        public MarkovSequence()
        {
        }

        private void OnRuleApplied(Vector2Int vector2Int, PatternDeformation2D deformation)
        {
            _onRuleApplied = true;
        }

        public override bool CanPlay(TSimType simulation)
        {
            var result = _onRuleApplied || _firstPlay;
            _firstPlay = false;
            return result;
        }

        private void UnsubRecursive(ISequence<TSimElement, TSimType> sequence)
        {
            foreach (var x in sequence.Playables.OfType<RuleBase<TSimElement>>())
                x.OnRuleApplied -= OnRuleApplied;
            foreach (var x in sequence.Playables.OfType<SequenceBase<TSimElement, TSimType>>())
                UnsubRecursive(x);
        }

        private void SubRecursive(ISequence<TSimElement, TSimType> sequence)
        {
            foreach (var x in sequence.Playables.OfType<RuleBase<TSimElement>>())
                x.OnRuleApplied += OnRuleApplied;
            foreach (var x in sequence.Playables.OfType<SequenceBase<TSimElement, TSimType>>())
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
            foreach (var x in Playables.OfType<RuleBase<TSimElement>>())
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