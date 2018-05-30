using System;
using Core;

namespace ProjectC
{
    public static class AdvancedExtensions
    {
        /// <summary>
        /// This function creates the square submatrix given a square matrix as
        /// well as row and column indices to remove from it.
        /// </summary>
        ///
        /// <remarks>
        /// See page 246-247 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <param name="a">An N-by-N matrix.</param>
        /// <param name="i">The index of the row to remove.</param>
        /// <param name="j">The index of the column to remove.</param>
        ///
        /// <returns>The resulting (N - 1)-by-(N - 1) submatrix.</returns>
        public static Matrix SquareSubMatrix(this Matrix a, int i, int j)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function computes the determinant of a given square matrix.
        /// </summary>
        ///
        /// <remarks>
        /// See page 247 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <remarks>
        /// Hint: Use SquareSubMatrix.
        /// </remarks>
        ///
        /// <param name="a">An N-by-N matrix.</param>
        ///
        /// <returns>The determinant of the matrix</returns>
        public static double Determinant(this Matrix a)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function computes the Gram-Schmidt process on a given matrix.
        /// </summary>
        ///
        /// <remarks>
        /// See page 229 in "Linear Algebra for Engineers and Scientists"
        /// by K. Hardy.
        /// </remarks>
        ///
        /// <param name="a">
        /// An M-by-N matrix. All columns are implicitly assumed linear
        /// independent.
        /// </param>
        ///
        /// <returns>
        /// A tuple (Q,R) where Q is a M-by-N orthonormal matrix and R is an
        /// N-by-N upper triangular matrix.
        /// </returns>
        public static Tuple<Matrix, Matrix> GramSchmidt(this Matrix a)
        {
            throw new NotImplementedException();
        }
    }
}
