using System;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
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
                    retMatrix[q, k] = a[
                        q >= i ? q + 1 : q, 
                        k >= j ? k + 1 : k
                    ];

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
            double retSum = 0.0;
            
            // Find determinant for submatrices for each entry in first row.
            for (int j = 0; j < a.M_Rows; j++)
            {
                // Finds cofactor. Matrix is indexed differently. row 1, column 1 is a[0,0] etc.
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
            // Q and R. Specifically named q and r for transparent implementation from algorithm on p. 229.
            Vector[] q = new Vector[a.N_Cols];
            Matrix r = new Matrix(a.N_Cols,a.N_Cols);
            
            // Matrices are indexed differently in this implementation compared to the pseudocode on p. 229.
            for (int j = 0; j < a.N_Cols; j++)
            {
                q[j] = a.Column(j);
                
                for (int i = 0; i < j; i++)
                {
                    // No need to transpose q[i] due to how dotted vectors work in this implementation.
                    r[i,j] = q[i] * a.Column(j);
                    q[j] = q[j] - r[i, j] * q[i];
                }
                // Ensures no division by 0
                if(Math.Abs(q[j].Norm()) <= 1e-10 ) continue;
                r[j,j] = q[j].Norm();
                q[j] = q[j] * (1/r[j, j]);
            }
            
            // Converts q to matrix and returns tuple with Q and R.
            return new Tuple<Matrix, Matrix>(VectorColCombine(q),r);
        }
        
        /// <summary>
        /// Calculates vector norm
        /// </summary>
        /// <param name="v">
        /// Vector to be processed.
        /// </param>
        /// <returns>
        /// Vector norm.
        /// </returns>
        private static double Norm(this Vector v)
        {
            double ret = 0.0;
            for (int i = 0; i < v.Size; i++) ret += v[i] * v[i];
            return Math.Sqrt(ret);
        }
    
        /// <summary>
        /// Converts list of vectors representing columns to an array.
        /// </summary>
        /// <param name="vecArr">
        /// List of vectors representing columns in an array.
        /// </param>
        /// <returns>
        /// Matrix created from the input array.
        /// </returns>
        private static Matrix VectorColCombine(Vector[] vecArr)
        {
            int colCount = vecArr.Length;
            int rowCount = colCount > 0 ? vecArr[0].Size : 0;
            
            double[,] retDoubleArr = new double[rowCount, colCount];
            
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    retDoubleArr[i, j] = vecArr[j][i];
            
            return new Matrix(retDoubleArr);    
        }

    }
}
