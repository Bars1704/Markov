using System;

namespace MarkovTest.TwoDimension.Patterns
{
    /// <summary>
    /// Contains methods to deform matrix, like rotating or mirroring
    /// </summary>
    /// <typeparam name="T">Type of matrix elements</typeparam>
    public static class MatrixFormatter<T>
    {
        /// <summary>
        /// Mirrors the given matrix by X axis without creating a copy of matrix
        /// </summary>
        /// <param name="matrix">Matrix, that will be mirrored</param>
        public static void MirrorNonAllocX(T[,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);

            for (var x = 0; x < sizeX; x++)
            for (var y = 0; y < sizeY / 2; y++)
                (matrix[x, y], matrix[x, sizeY - y - 1]) = (matrix[x, sizeY - y - 1], matrix[x, y]);
        }

        public static T[,] Resize<T>(T[,] matrix, int sizeX, int sizeY)
        {
            var res = new T[sizeX, sizeY];
            var minX = Math.Min(sizeX, matrix.GetLength(0));
            var minY = Math.Min(sizeY, matrix.GetLength(1));

            for (int x = 0; x < minX; x++)
            for (int y = 0; y < minY; y++)
                res[x, y] = matrix[x, y];

            return res;
        }

        /// <summary>
        /// Mirrors the given matrix by Y axis without creating a copy of matrix
        /// </summary>
        /// <param name="matrix">Matrix, that will be mirrored</param>
        public static void MirrorNonAllocY(T[,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);

            for (var x = 0; x < sizeX / 2; x++)
            for (var y = 0; y < sizeY; y++)
                (matrix[x, y], matrix[sizeX - x - 1, y]) = (matrix[sizeX - x - 1, y], matrix[x, y]);
        }

        /// <summary>
        /// Mirrors the given matrix by Y axis
        /// </summary>
        /// <param name="matrix">Matrix, that will be mirrored</param>
        /// <returns>Mirrored copy of matrix</returns>
        /// <example>
        /// [[1,2],[3,4]] => [[3,4],[1,2]]
        /// </example>
        public static T[,] MirrorY(T[,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);

            var copy = new T[sizeX, sizeY];
            Array.Copy(matrix, copy, matrix.Length);

            MirrorNonAllocY(copy);

            return copy;
        }

        /// <summary>
        /// Mirrors the given matrix by X axis
        /// </summary>
        /// <param name="matrix">Matrix, that will be mirrored</param>
        /// <returns>Mirrored copy of matrix</returns>
        /// <example>[[1,2],[3,4]] => [[2,1],[4,3]]</example>
        public static T[,] MirrorX(T[,] matrix)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);

            var copy = new T[sizeX, sizeY];
            Array.Copy(matrix, copy, matrix.Length);

            MirrorNonAllocX(copy);

            return copy;
        }

        /// <summary>
        /// Rotate matrix on some angle
        /// </summary>
        /// <param name="matrix">Matrix, that will be rotated</param>
        /// <param name="rotationAngle">Rotation angle</param>
        /// <returns>New Rotated Array</returns>
        /// <exception cref="ArgumentOutOfRangeException">throws if <paramref name="rotationAngle"/> is invalid</exception>
        /// <example>  [[1,2],[3,4]] rotates on 90 degrees => [[3,1],[4,2]]</example>
        public static T[,] Rotate(T[,] matrix, RotationAngle rotationAngle)
        {
            var sizeX = matrix.GetLength(0);
            var sizeY = matrix.GetLength(1);

            switch (rotationAngle)
            {
                case RotationAngle.Degrees0:
                    var copy = new T[sizeX, sizeY];
                    Array.Copy(matrix, copy, matrix.Length);
                    return copy;
                case RotationAngle.Degrees90:
                    copy = new T[sizeY, sizeX];
                    for (var x = 0; x < sizeY; x++)
                    for (var y = 0; y < sizeX; y++)
                        copy[x, y] = matrix[sizeX - y - 1, x];
                    return copy;
                case RotationAngle.Degrees180:
                    copy = new T[sizeX, sizeY];
                    for (var x = 0; x < sizeX; x++)
                    for (var y = 0; y < sizeY; y++)
                        copy[x, y] = matrix[sizeX - x - 1, sizeY - y - 1];
                    return copy;
                case RotationAngle.Degrees270:
                    copy = new T[sizeY, sizeX];
                    for (var x = 0; x < sizeY; x++)
                    for (var y = 0; y < sizeX; y++)
                        copy[x, y] = matrix[y, sizeY - x - 1];
                    return copy;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rotationAngle), rotationAngle, null);
            }
        }
    }
}