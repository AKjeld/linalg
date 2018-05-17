using System;

namespace Core
{
    public static class MatrixFactory
    {
        /// <summary>
        /// Create an n-by-n identity matrix.
        /// </summary>
        /// 
        /// <remarks>
        /// See page 64 in "Linear Algebra for Engineers and
        /// Scientists" by K. Hardy.
        /// </remarks>
        public static Matrix Identity(int n)
        {
            var retval = new Matrix(n, n);
            for (var i = 0; i < n; i++)
            {
                retval[i, i] = 1.0;
            }
            return retval;
        }

        /// <summary>
        /// Create an n-by-n Hilbert matrix.
        /// </summary>
        /// 
        /// <remarks>
        /// See https://en.wikipedia.org/wiki/Hilbert_matrix.
        /// </remarks>
        public static Matrix Hilbert(int n)
        {
            var retval = new Matrix(n, n);
            for (var i = 0; i < n; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    retval[i, j] = 1.0 / (i + j + 1.0);
                }
            }
            return retval;
        }
    }
}