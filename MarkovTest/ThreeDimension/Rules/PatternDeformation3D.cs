using System;

namespace MarkovTest.ThreeDimension.Rules
{
    public  struct PatternDeformation3D : IEquatable<PatternDeformation3D>
    {
        public  RotationAngle RotationAngleX;
        public  RotationAngle RotationAngleY;
        public  RotationAngle RotationAngleZ;
        public  bool FlipX;
        public  bool FlipY;
        public  bool FlipZ;

        public PatternDeformation3D(RotationAngle rotationAngleX, RotationAngle rotationAngleY,
            RotationAngle rotationAngleZ, bool flipX, bool flipY,
            bool flipZ)
        {
            RotationAngleX = rotationAngleX;
            RotationAngleY = rotationAngleY;
            RotationAngleZ = rotationAngleZ;
            FlipX = flipX;
            FlipY = flipY;
            FlipZ = flipZ;
        }

        public override string ToString() =>
            $"PatternDeformation: " +
            $"RotationX:{RotationAngleX.ToString()} " +
            $"RotationY:{RotationAngleY.ToString()}" +
            $"RotationZ:{RotationAngleZ.ToString()}" +
            $"{(FlipX ? ", FlippedX" : string.Empty)}" +
            $"{(FlipY ? ", FlippedY" : string.Empty)}" +
            $"{(FlipZ ? ", FlippedZ" : string.Empty)}";

        #region Equality Members

        public bool Equals(PatternDeformation3D other)
        {
            return RotationAngleX == other.RotationAngleX &&
                   RotationAngleY == other.RotationAngleY &&
                   RotationAngleZ == other.RotationAngleZ &&
                   FlipX == other.FlipX &&
                   FlipY == other.FlipY &&
                   FlipZ == other.FlipZ;
        }

        public override bool Equals(object? obj)
        {
            return obj is PatternDeformation3D other && Equals(other);
        }

        public override int GetHashCode()
        {
            return ((int)RotationAngleX, (int)RotationAngleY, (int)RotationAngleZ, FlipX, FlipY, FlipZ).GetHashCode();
        }

        public static bool operator ==(PatternDeformation3D left, PatternDeformation3D right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(PatternDeformation3D left, PatternDeformation3D right)
        {
            return !left.Equals(right);
        }

        #endregion
    }
}