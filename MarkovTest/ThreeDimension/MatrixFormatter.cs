using System;

namespace MarkovTest.ThreeDimension
{
    /// <summary>
    /// Contains methods to deform matrix, like rotating or mirroring
    /// </summary>
    /// <typeparam name="T">Type of matrix elements</typeparam>
    public static partial class MatrixFormatter<T>
    {
        public static T[,,] Resize(T[,,] matrix, int sizeX, int sizeY, int sizeZ)
        {
            var res = new T[sizeX, sizeY, sizeZ];
            var minX = Math.Min(sizeX, matrix.GetLength(0));
            var minY = Math.Min(sizeY, matrix.GetLength(1));
            var minZ = Math.Min(sizeZ, matrix.GetLength(2));

            for (int x = 0; x < minX; x++)
            for (int y = 0; y < minY; y++)
            for (int z = 0; z < minZ; z++)
                res[x, y, z] = matrix[x, y, z];

            return res;
        }

        public static void MirrorNonAllocX(T[,,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            for (var x = 0; x < sizeX / 2; x++)
            for (var y = 0; y < sizeY; y++)
            for (var z = 0; z < sizeZ; z++)
                (matrix[sizeX - x - 1, y, z], matrix[x, y, z]) = (matrix[sizeX - x - 1, y, z], matrix[x, y, z]);
        }

        public static T[,,] MirrorX(T[,,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            var copy = new T[sizeX, sizeY, sizeZ];
            Array.Copy(matrix, copy, matrix.Length);

            MirrorNonAllocX(copy);

            return copy;
        }

        public static void MirrorNonAllocY(T[,,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            for (var x = 0; x < sizeX; x++)
            for (var y = 0; y < sizeY / 2; y++)
            for (var z = 0; z < sizeZ; z++)
                (matrix[x, y, z], matrix[x, sizeY - y - 1, z]) = (matrix[x, sizeY - y - 1, z], matrix[x, y, z]);
        }


        public static T[,,] MirrorY(T[,,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            var copy = new T[sizeX, sizeY, sizeZ];
            Array.Copy(matrix, copy, matrix.Length);

            MirrorNonAllocY(copy);

            return copy;
        }


        public static void MirrorNonAllocZ(T[,,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            for (var x = 0; x < sizeX; x++)
            for (var y = 0; y < sizeY; y++)
            for (var z = 0; z < sizeZ / 2; z++)
                (matrix[x, y, z], matrix[x, y, sizeZ - z - 1]) = (matrix[x, y, sizeZ - z - 1], matrix[x, y, z]);
        }


        public static T[,,] MirrorZ(T[,,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            var copy = new T[sizeX, sizeY, sizeZ];
            Array.Copy(matrix, copy, matrix.Length);

            MirrorNonAllocZ(copy);

            return copy;
        }


        public static T[,,] RotateX(T[,,] matrix, RotationAngle rotationAngle)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            switch (rotationAngle)
            {
                case RotationAngle.Degrees0:
                    var copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees90:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees180:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees270:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotationAngle), rotationAngle, null);
            }
        }

        public static T[,,] RotateY(T[,,] matrix, RotationAngle rotationAngle)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            switch (rotationAngle)
            {
                case RotationAngle.Degrees0:
                    var copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees90:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees180:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees270:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotationAngle), rotationAngle, null);
            }
        }

        public static T[,,] RotateZ(T[,,] matrix, RotationAngle rotationAngle)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);
            var sizeZ = matrix.GetLength(2);

            switch (rotationAngle)
            {
                case RotationAngle.Degrees0:
                    var copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees90:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees180:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees270:
                    copy = new T[sizeX, sizeY, sizeZ];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotationAngle), rotationAngle, null);
            }
        }
    }
}