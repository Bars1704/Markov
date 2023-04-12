using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using MarkovTest.Converters;
using MarkovTest.TwoDimension.Rules;
using Newtonsoft.Json;

namespace MarkovTest.TwoDimension.Patterns
{
    //TODO: подумать, как сделать тут абстракцию
    /// <summary>
    /// Represents 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pattern<T> : IResizable where T : IEquatable<T>
    {
        [JsonConverter(typeof(PatternConverter2D))]
        public IEquatable<T>[,] PatternForm { get; set; }

        [JsonProperty] public RotationSettingsFlags RotationSettings { get; set; }

        [JsonIgnore] public Dictionary<PatternDeformation2D, IEquatable<T>[,]> Patterns { get; set; }

        public Pattern(IEquatable<T>[,] patternForm,
            RotationSettingsFlags rotationSettings = RotationSettingsFlags.None)
        {
            RotationSettings = rotationSettings;
            PatternForm = patternForm;

            CachePatternDeformations();
        }

        public Pattern()
        {
            PatternForm = new IEquatable<T>[0, 0];
            RotationSettings = RotationSettingsFlags.None;
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            CachePatternDeformations();
        }


        /// <summary>
        /// Check, if there pattern in given 
        /// </summary>
        /// <param name="simulation">Checked simulation</param>
        /// <param name="coord">Checked coords (top-left corner)</param>
        /// <returns>True if there is given pattern in given coords, false if not</returns>
        public virtual bool Compare(MarkovSimulation<T> simulation, Vector2Int coord)
        {
            if (simulation.Size.X <= coord.X + PatternForm.GetLength(0) - 1)
                return false;

            if (simulation.Size.Y <= coord.Y + PatternForm.GetLength(1) - 1)
                return false;

            for (var x = 0; x < PatternForm.GetLength(0); x++)
            {
                for (var y = 0; y < PatternForm.GetLength(1); y++)
                {
                    var flag = simulation[coord.X + x, coord.Y + y].Equals(PatternForm[x, y]) ||
                               simulation[coord.X + x, coord.Y + y].Equals(default(T)) ||
                               PatternForm[x, y].Equals(default(T));

                    if (!flag)
                        return false;
                }
            }

            return true;
        }


        public void Resize(Vector2Int newSize)
        {
            PatternForm = MatrixFormatter<IEquatable<T>>.Resize(PatternForm, newSize.X, newSize.Y);
            for (var x = 0; x < PatternForm.GetLength(0); x++)
            for (var y = 0; y < PatternForm.GetLength(1); y++)
                if (PatternForm[x, y] is null)
                    PatternForm[x, y] = default(T);

            CachePatternDeformations();
        }

        public Vector2Int Size => new Vector2Int(PatternForm.GetLength(0), PatternForm.GetLength(1));

        private IEnumerable<(RotationAngle rotationType, IEquatable<T>[,]pattern)> RotatePatterns(
            IEquatable<T>[,] pattern)
        {
            (RotationAngle rType, IEquatable<T>[,]) Rotate(RotationAngle rType) =>
                (rType, MatrixFormatter<IEquatable<T>>.Rotate(pattern, rType));

            yield return Rotate(RotationAngle.Degrees0);
            yield return Rotate(RotationAngle.Degrees90);
            yield return Rotate(RotationAngle.Degrees180);
            yield return Rotate(RotationAngle.Degrees270);
        }

        public IEnumerable<(Vector2Int coord, PatternDeformation2D RotationSettings)> GetAllCoords(MarkovSimulation<T> simulation)
        {
            return Patterns.SelectMany(x =>
                GetPatternCoords(simulation)
                    .Select(y => (coord: y, RotationSettings: x.Key)));
        }


        /// <summary>
        /// Returns coordinates, that matches to pattern
        /// </summary>
        /// <param name="pattern">Pattens, that will be checked</param>
        /// <returns> Coordinates, that matches to pattern</returns>
        public IEnumerable<Vector2Int> GetPatternCoords(MarkovSimulation<T> simulation)
        {
            for (var x = 0; x < simulation.Simulation.GetLength(0); x++)
            {
                for (var y = 0; y < simulation.Simulation.GetLength(1); y++)
                {
                    var coord = new Vector2Int(x, y);
                    if (Compare(simulation, coord))
                        yield return coord;
                }
            }
        }

        private void CachePatternDeformations()
        {
            Patterns = new Dictionary<PatternDeformation2D, IEquatable<T>[,]>();

            if (RotationSettings.HasFlag(RotationSettingsFlags.Rotate))
                foreach (var rotatePattern in RotatePatterns(PatternForm))
                    Patterns.Add(new PatternDeformation2D(rotatePattern.rotationType, false, false),
                        rotatePattern.pattern);
            else
                Patterns.Add(new PatternDeformation2D(RotationAngle.Degrees0, false, false), PatternForm);

            var patternList = new List<(PatternDeformation2D, IEquatable<T>[,])>(Patterns.Count);
            if (RotationSettings.HasFlag(RotationSettingsFlags.FlipX))
                foreach (var pattern in Patterns)
                {
                    var (s, p) = (pattern.Key, pattern.Value);
                    var settingsCopy = new PatternDeformation2D(s.RotationAngle, true, s.FlipY);
                    var copy = MatrixFormatter<IEquatable<T>>.MirrorX(PatternForm);
                    patternList.Add((settingsCopy, copy));
                }

            patternList.ForEach(x => Patterns.Add(x.Item1, x.Item2));

            patternList.Clear();
            if (RotationSettings.HasFlag(RotationSettingsFlags.FlipY))
                foreach (var pattern in Patterns)
                {
                    var (s, p) = (pattern.Key, pattern.Value);
                    var settingsCopy = new PatternDeformation2D(s.RotationAngle, s.FlipX, true);
                    var copy = MatrixFormatter<IEquatable<T>>.MirrorY(PatternForm);
                    patternList.Add((settingsCopy, copy));
                }

            patternList.ForEach(x => Patterns.Add(x.Item1, x.Item2));
        }
    }
}