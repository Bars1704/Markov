using System;
using MarkovTest.Converters;
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
        [JsonConverter(typeof(PatternConverter))]
        public IEquatable<T>[,] PatternForm { get; set; }

        public Pattern(IEquatable<T>[,] patternForm)
        {
            PatternForm = patternForm;
        }

        public Pattern()
        {
            PatternForm = new IEquatable<T>[0, 0];
        }

        /// <summary>
        /// Check, if there pattern in given 
        /// </summary>
        /// <param name="simulation">Checked simulation</param>
        /// <param name="coord">Checked coords (top-left corner)</param>
        /// <returns>True if there is given pattern in given coords, false if not</returns>
        public virtual bool Compare(MarkovSimulationTwoDim<T> simulation, Vector2Int coord)
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
            PatternForm = MatrixFormatter<T>.Resize(PatternForm, newSize.X, newSize.Y);
            for (var x = 0; x < PatternForm.GetLength(0); x++)
            for (var y = 0; y < PatternForm.GetLength(1); y++)
                if (PatternForm[x, y] is null)
                    PatternForm[x, y] = default(T);
        }

        public Vector2Int Size => new Vector2Int(PatternForm.GetLength(0), PatternForm.GetLength(1));
    }
}