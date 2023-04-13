// ReSharper disable FieldCanBeMadeReadOnly.Global

using System;

namespace Markov.MarkovTest.TwoDimension
{
    /// <summary>
    /// Structure,that contains 2 integer values, represents coordinates in 2-dimensional space
    /// </summary>
    public struct Vector2Int : IEquatable<Vector2Int>
    {
        /// <summary>
        /// First number of vector (represents X coordinate)
        /// </summary>
        public int X;

        /// <summary>
        /// Second number of vector (represents Y coordinate)
        /// </summary>
        public int Y;

        public Vector2Int(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"Vector2Int ({X},{Y})";

        #region EqualityMembes

        public bool Equals(Vector2Int other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector2Int other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (X, Y).GetHashCode();
        }

        public static bool operator ==(Vector2Int left, Vector2Int right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2Int left, Vector2Int right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}