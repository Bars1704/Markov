using System.Runtime.Serialization;
using MarkovTest.TwoDimension.Patterns;
using MarkovTest.TwoDimension.Sequences;
using Newtonsoft.Json;
using System;

namespace MarkovTest.TwoDimension.Rules
{
    public abstract class RuleBase<T> : ISequencePlayable<T> where T : IEquatable<T>
    {
        [JsonProperty] public RotationSettingsFlags RotationSettings { get; }

        [JsonProperty] public T[,] Stamp { get; }

        [JsonProperty] public Pattern<T> MainPattern { get; private set; }

        [JsonIgnore] public Dictionary<PatternDeformation, Pattern<T>> Patterns { get; set; }

        private IEnumerable<(RotationAngle rotationType, Pattern<T> pattern)> RotatePatterns(Pattern<T> pattern)
        {
            (RotationAngle rType, Pattern<T>) Rotate(RotationAngle rType) =>
                (rType, new Pattern<T>(MatrixFormatter<IEquatable<T>>.Rotate(pattern.PatternForm, rType)));

            yield return Rotate(RotationAngle.Degrees0);
            yield return Rotate(RotationAngle.Degrees90);
            yield return Rotate(RotationAngle.Degrees180);
            yield return Rotate(RotationAngle.Degrees270);
        }

        protected RuleBase(Pattern<T> mainPattern, T[,] stamp, RotationSettingsFlags rotationSettings)
        {
            RotationSettings = rotationSettings;
            Stamp = stamp;
            MainPattern = mainPattern;
            if (mainPattern != default)
                CachePatternDeformations();
        }

        protected RuleBase()
        {
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            CachePatternDeformations();
        }

        private void CachePatternDeformations()
        {
            Patterns = new Dictionary<PatternDeformation, Pattern<T>>();

            if (RotationSettings.HasFlag(RotationSettingsFlags.Rotate))
                foreach (var rotatePattern in RotatePatterns(MainPattern))
                    Patterns.Add(new PatternDeformation(rotatePattern.rotationType, false, false),
                        rotatePattern.pattern);
            else
                Patterns.Add(new PatternDeformation(RotationAngle.Degrees0, false, false), MainPattern);

            var patternList = new List<(PatternDeformation, Pattern<T>)>(Patterns.Count);
            if (RotationSettings.HasFlag(RotationSettingsFlags.FlipX))
                foreach (var (s, p) in Patterns)
                {
                    var settingsCopy = new PatternDeformation(s.RotationAngle, true, s.FlipY);
                    var copy = MatrixFormatter<IEquatable<T>>.MirrorX(p.PatternForm);
                    patternList.Add((settingsCopy, new Pattern<T>(copy)));
                }

            patternList.ForEach(x => Patterns.Add(x.Item1, x.Item2));

            patternList.Clear();
            if (RotationSettings.HasFlag(RotationSettingsFlags.FlipY))
                foreach (var (s, p) in Patterns)
                {
                    var settingsCopy = new PatternDeformation(s.RotationAngle, s.FlipX, true);
                    var copy = MatrixFormatter<IEquatable<T>>.MirrorY(p.PatternForm);
                    patternList.Add((settingsCopy, new Pattern<T>(copy)));
                }

            patternList.ForEach(x => Patterns.Add(x.Item1, x.Item2));
        }

        protected void ApplyRuleEvent(Vector2Int coord, PatternDeformation rotationSettings)
        {
            OnRuleApplied?.Invoke(coord, rotationSettings);
            OnPlayed?.Invoke();
        }

        public event Action<Vector2Int, PatternDeformation> OnRuleApplied;

        public event Action? OnPlayed;
        public abstract void Play(MarkovSimulationTwoDim<T> simulation);
    }
}