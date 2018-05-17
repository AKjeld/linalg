using System;
using Core;

namespace ProjectA
{
    public static class BasicExtensions
    {
        /// <summary>
        /// This function creates an augmented matrix given a matrix 'a' and a
        /// right-hand side vector 'v'.
        /// </summary>
        ///
        /// <remarks>
        /// See page 12 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <param name="a">An M-by-N matrix.</param>
        /// <param name="v">An M-size vector.</param>
        ///
        /// <returns>The M-by-(N + 1) augmented matrix [a | v].</returns>
        public static Matrix AugmentRight(this Matrix a, Vector v)
        {
            var mRows = a.M_Rows;
            var nCols = a.N_Cols;

            var retval = new double[mRows, nCols + 1]; // 0-initialized

            for (var i = 0; i < mRows; i++)
            {
                for (var j = 0; j < nCols; j++)
                {
                    retval[i, j] = a[i, j];
                }
                retval[i, nCols] = v[i];
            }

            return new Matrix(retval);
        }

        /// <summary>
        /// This function computes the matrix-vector product of a matrix 'a' and
        /// a column vector 'v'.
        /// </summary>
        ///
        /// <remarks>
        /// See page 68 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <param name="a">An M-by-N matrix.</param>
        /// <param name="v">An N-size vector.</param>
        ///
        /// <returns>The M-sized vector a * v.</returns>
        public static Vector Product(this Matrix a, Vector v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function computes the matrix product of two given matrices 'a'
        /// and 'b'.
        /// </summary>
        ///
        /// <remarks>
        /// See page 58 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <param name="a">An M-by-N matrix.</param>
        /// <param name="b">An N-by-P matrix.</param>
        ///
        /// <returns>The M-by-P matrix a * b.</returns>
        public static Matrix Product(this Matrix a, Matrix b)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function computes the transpose of a given matrix.
        /// </summary>
        ///
        /// <remarks>
        /// See page 69 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <param name="a">An M-by-N matrix.</param>
        ///
        /// <returns>The N-by-M matrix a^T.</returns>
        public static Matrix Transpose(this Matrix a)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function computes the Euclidean vector norm of a given vector.
        /// </summary>
        ///
        /// <remarks>
        /// See page 197 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <param name="v">An N-dimensional vector.</param>
        ///
        /// <returns>The Euclidean norm of the vector.</returns>
        public static double VectorNorm(this Vector v)
        {
            throw new NotImplementedException();
        }
    }
}
