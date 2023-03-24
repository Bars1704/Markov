// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace MarkovTest.ThreeDimension
{
    /// <summary>
    /// Structure,that contains 3 integer values, represents coordinates in 3-dimensional space
    /// </summary>
    public struct Vector3Int : IEquatable<Vector3Int>
    {
        /// <summary>
        /// First number of vector (represents X coordinate)
        /// </summary>
        public int X;

        /// <summary>
        /// Second number of vector (represents Y coordinate)
        /// </summary>
        public int Y;

        /// <summary>
        /// Third number of vector (represents Z coordinate)
        /// </summary>
        public int Z;

        public Vector3Int(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString() => $"Vector2Int ({X},{Y},{Z})";

        #region EqualityMembes

        public bool Equals(Vector3Int other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object? obj)
        {
            return obj is Vector3Int other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public static bool operator ==(Vector3Int left, Vector3Int right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector3Int left, Vector3Int right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}