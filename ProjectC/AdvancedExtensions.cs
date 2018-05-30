using System;
using System.Security.AccessControl;
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
            // Finds dimensions of return matrix.
            // Generates matrix with new dimensions
            Matrix retMatrix = new Matrix(a.M_Rows - 1,a.N_Cols - 1);

            // Fills each cell of return matrix
            for (int q = 0; q < retMatrix.M_Rows; q++)
                for (int k = 0; k < retMatrix.N_Cols; k++)
                    
                    // Fills cells from matrix a.
                    // Takes one index higher for rows and cols if row or col has been removed.
                    retMatrix[q, k] = a[q >= i ? q + 1 : q, k >= j ? k + 1: k];

            return retMatrix;
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
            // Recursive step. If 1x1 matrix is found a[0,0] is the determinant
            if (a.M_Rows <= 1) return a[0,0];
            
            // Running sum of current matrix' determinant
            var retSum = 0.0;
            
            // Find determinant for submatrices for each entry in first row.
            for (int j = 0; j < a.M_Rows; j++)
            {
                // Finds cofactor
                double c = Math.Pow(-1, j) * Determinant(a.SquareSubMatrix(0,j));
                retSum += a[0,j]*c;
            }

            return retSum;
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
