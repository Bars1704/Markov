using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Markov.MarkovTest.Converters;
using Markov.MarkovTest.ThreeDimension.Rules;
using Newtonsoft.Json;

namespace Markov.MarkovTest.ThreeDimension.Patterns
{
    //TODO: подумать, как сделать тут абстракцию
    /// <summary>
    /// Represents 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pattern<T> : IResizable where T : IEquatable<T>
    {
        [JsonConverter(typeof(PatternConverter3D))]
        public IEquatable<T>[,,] PatternForm { get; set; }

        [JsonProperty] public RotationSettingsFlags RotationSettings { get; set; }

        [JsonIgnore] public Dictionary<PatternDeformation3D, IEquatable<T>[,,]> Patterns { get; set; }

        public Pattern(IEquatable<T>[,,] patternForm,
            RotationSettingsFlags rotationSettings = RotationSettingsFlags.None)
        {
            RotationSettings = rotationSettings;
            PatternForm = patternForm;

            CachePatternDeformations();
        }

        public Pattern()
        {
            PatternForm = new IEquatable<T>[1, 1, 1];
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
        public virtual bool Compare(MarkovSimulation<T> simulation, Vector3Int coord)
        {
            if (simulation.Size.X <= coord.X + PatternForm.GetLength(0) - 1)
                return false;

            if (simulation.Size.Y <= coord.Y + PatternForm.GetLength(1) - 1)
                return false;

            if (simulation.Size.Z <= coord.Z + PatternForm.GetLength(2) - 1)
                return false;

            for (var x = 0; x < PatternForm.GetLength(0); x++)
            {
                for (var y = 0; y < PatternForm.GetLength(1); y++)
                {
                    for (var z = 0; z < PatternForm.GetLength(2); z++)
                    {
                        var flag = simulation[coord.X + x, coord.Y + y, coord.Z + z].Equals(PatternForm[x, y, z]) ||
                                   simulation[coord.X + x, coord.Y + y, coord.Z + z].Equals(default(T)) ||
                                   PatternForm[x, y, z].Equals(default(T));

                        if (!flag)
                            return false;
                    }
                }
            }

            return true;
        }


        public void Resize(Vector3Int newSize)
        {
            PatternForm = MatrixFormatter<IEquatable<T>>.Resize(PatternForm, newSize.X, newSize.Y, newSize.Z);
            for (var x = 0; x < PatternForm.GetLength(0); x++)
            for (var y = 0; y < PatternForm.GetLength(1); y++)
            for (var z = 0; z < PatternForm.GetLength(2); z++)
                PatternForm[x, y, z] ??= default(T);

            CachePatternDeformations();
        }

        public Vector3Int Size =>
            new Vector3Int(PatternForm.GetLength(0), PatternForm.GetLength(1), PatternForm.GetLength(2));

        private static IEnumerable<(RotationAngle rotationType, IEquatable<T>[,,]pattern)> RotatePatternsX(
            IEquatable<T>[,,] pattern)
        {
            (RotationAngle rType, IEquatable<T>[,,]) Rotate(RotationAngle rType) =>
                (rType, MatrixFormatter<IEquatable<T>>.RotateX(pattern, rType));

            yield return Rotate(RotationAngle.Degrees0);
            yield return Rotate(RotationAngle.Degrees90);
            yield return Rotate(RotationAngle.Degrees180);
            yield return Rotate(RotationAngle.Degrees270);
        }

        private static IEnumerable<(RotationAngle rotationType, IEquatable<T>[,,]pattern)> RotatePatternsY(
            IEquatable<T>[,,] pattern)
        {
            (RotationAngle rType, IEquatable<T>[,,]) Rotate(RotationAngle rType) =>
                (rType, MatrixFormatter<IEquatable<T>>.RotateY(pattern, rType));

            yield return Rotate(RotationAngle.Degrees0);
            yield return Rotate(RotationAngle.Degrees90);
            yield return Rotate(RotationAngle.Degrees180);
            yield return Rotate(RotationAngle.Degrees270);
        }

        private static IEnumerable<(RotationAngle rotationType, IEquatable<T>[,,]pattern)> RotatePatternsZ(
            IEquatable<T>[,,] pattern)
        {
            (RotationAngle rType, IEquatable<T>[,,]) Rotate(RotationAngle rType) =>
                (rType, MatrixFormatter<IEquatable<T>>.RotateZ(pattern, rType));

            yield return Rotate(RotationAngle.Degrees0);
            yield return Rotate(RotationAngle.Degrees90);
            yield return Rotate(RotationAngle.Degrees180);
            yield return Rotate(RotationAngle.Degrees270);
        }

        public IEnumerable<(Vector3Int coord, PatternDeformation3D RotationSettings)> GetAllCoords(
            MarkovSimulation<T> simulation)
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
        public IEnumerable<Vector3Int> GetPatternCoords(MarkovSimulation<T> simulation)
        {
            for (var x = 0; x < simulation.Simulation.GetLength(0); x++)
            {
                for (var y = 0; y < simulation.Simulation.GetLength(1); y++)
                {
                    for (var z = 0; z < simulation.Simulation.GetLength(2); z++)
                    {
                        var coord = new Vector3Int(x, y, z);
                        if (Compare(simulation, coord))
                            yield return coord;
                    }
                }
            }
        }

        private void CachePatternDeformations()
        {
            Patterns = new Dictionary<PatternDeformation3D, IEquatable<T>[,,]>();

            if (RotationSettings.HasFlag(RotationSettingsFlags.RotateX))
                foreach (var rotatePattern in RotatePatternsX(PatternForm))
                    Patterns.Add(
                        new PatternDeformation3D(rotatePattern.rotationType, RotationAngle.Degrees0,
                            RotationAngle.Degrees0, false, false, false),
                        rotatePattern.pattern);
            else
                Patterns.Add(
                    new PatternDeformation3D(RotationAngle.Degrees0, RotationAngle.Degrees0, RotationAngle.Degrees0,
                        false, false, false), PatternForm);


            if (RotationSettings.HasFlag((RotationSettingsFlags.RotateY)))
            {
                foreach (var pattern in Patterns)
                {
                    foreach (var rotatePattern in RotatePatternsY(pattern.Value))
                    {
                        var newDef = pattern.Key;
                        newDef.RotationAngleY = rotatePattern.rotationType;
                        Patterns.Add(newDef, rotatePattern.pattern);
                    }
                }
            }


            if (RotationSettings.HasFlag((RotationSettingsFlags.RotateZ)))
            {
                foreach (var pattern in Patterns)
                {
                    foreach (var rotatePattern in RotatePatternsZ(pattern.Value))
                    {
                        var newDef = pattern.Key;
                        newDef.RotationAngleZ = rotatePattern.rotationType;
                        Patterns.Add(newDef, rotatePattern.pattern);
                    }
                }
            }

            if (RotationSettings.HasFlag(RotationSettingsFlags.FlipX))
            {
                foreach (var pattern in Patterns)
                {
                    var newDef = pattern.Key;
                    newDef.FlipX = true;
                    Patterns.Add(newDef, MatrixFormatter<IEquatable<T>>.MirrorX(pattern.Value));
                }
            }
            
            if (RotationSettings.HasFlag(RotationSettingsFlags.FlipY))
            {
                foreach (var pattern in Patterns)
                {
                    var newDef = pattern.Key;
                    newDef.FlipY = true;
                    Patterns.Add(newDef, MatrixFormatter<IEquatable<T>>.MirrorY(pattern.Value));
                }
            }
            
            if (RotationSettings.HasFlag(RotationSettingsFlags.FlipZ))
            {
                foreach (var pattern in Patterns)
                {
                    var newDef = pattern.Key;
                    newDef.FlipZ = true;
                    Patterns.Add(newDef, MatrixFormatter<IEquatable<T>>.MirrorZ(pattern.Value));
                }
            }
        }
    }
}