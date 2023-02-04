using MarkovTest.TwoDimension.Patterns;

namespace MarkovTest.TwoDimension.Rules
{
    public readonly struct PatternDeformation : IEquatable<PatternDeformation>
    {
        public readonly RotationAngle RotationAngle;
        public readonly bool FlipX;
        public readonly bool FlipY;

        public PatternDeformation(RotationAngle rotationAngle, bool FlipX, bool FlipY)
        {
            this.RotationAngle = rotationAngle;
            this.FlipX = FlipX;
            this.FlipY = FlipY;
        }

        public override string ToString() =>
            $"PatternDeformation: Rotation:{RotationAngle.ToString()}{(FlipX ? ", FlippedX" : string.Empty)}{(FlipY ? ", FlippedY" : string.Empty)}";

        #region Equality Members

        public bool Equals(PatternDeformation other)
        {
            return RotationAngle == other.RotationAngle && FlipX == other.FlipX && FlipY == other.FlipY;
        }

        public override bool Equals(object? obj)
        {
            return obj is PatternDeformation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int)RotationAngle, FlipX, FlipY);
        }

        public static bool operator ==(PatternDeformation left, PatternDeformation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PatternDeformation left, PatternDeformation right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}