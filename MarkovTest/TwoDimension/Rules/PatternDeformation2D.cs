using MarkovTest.TwoDimension.Patterns;
using System;

namespace MarkovTest.TwoDimension.Rules
{
    public readonly struct PatternDeformation2D : IEquatable<PatternDeformation2D>
    {
        public readonly RotationAngle RotationAngle;
        public readonly bool FlipX;
        public readonly bool FlipY;

        public PatternDeformation2D(RotationAngle rotationAngle, bool FlipX, bool FlipY)
        {
            this.RotationAngle = rotationAngle;
            this.FlipX = FlipX;
            this.FlipY = FlipY;
        }

        public override string ToString() =>
            $"PatternDeformation: Rotation:{RotationAngle.ToString()}{(FlipX ? ", FlippedX" : string.Empty)}{(FlipY ? ", FlippedY" : string.Empty)}";

        #region Equality Members

        public bool Equals(PatternDeformation2D other)
        {
            return RotationAngle == other.RotationAngle && FlipX == other.FlipX && FlipY == other.FlipY;
        }

        public override bool Equals(object? obj)
        {
            return obj is PatternDeformation2D other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ((int)RotationAngle, FlipX, FlipY).GetHashCode();
        }

        public static bool operator ==(PatternDeformation2D left, PatternDeformation2D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PatternDeformation2D left, PatternDeformation2D right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}