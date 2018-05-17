using System;
using System.CodeDom;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Core;

namespace ProjectB
{
    public static class GaussExtensions
    {
        /// <summary>
        /// This function computes the elementary row replacement operation on
        /// the given matrix.
        /// </summary>
        ///
        /// <remarks>
        /// Note that we add the row (as in the lectures) instead of subtracting
        /// the row (as in the textbook).
        /// </remarks>
        ///
        /// <param name="a">
        /// An N-by-M matrix to perform the elementary row operation on.
        /// </param>
        /// <param name="i">
        /// The index of the row to replace.
        /// </param>
        /// <param name="m">
        /// The multiplum of row j to add to row i.
        /// </param>
        /// <param name="j">
        /// The index of the row to replace with.
        /// </param>
        ///
        /// <returns>
        /// The resulting N-by-M matrix after having performed the elementary
        /// row operation.
        /// </returns>
        
        public static Matrix ElementaryRowReplacement(
            this Matrix a, int i, double m, int j)
        {
            return a.InsertRow(i, a.Row(i) + a.Row(j) * m);
        }

        /// <summary>
        /// This function computes the elementary row interchange operation on
        /// the given matrix.
        /// </summary>
        ///
        /// <param name="a">
        /// An N-by-M matrix to perform the elementary row operation on.
        /// </param>
        /// <param name="i">
        /// The index of the first row of the rows to interchange.
        /// </param>
        /// <param name="j">
        /// The index of the second row of the rows to interchange.
        /// </param>
        ///
        /// <returns>
        /// The resulting N-by-M matrix after having performed the elementary
        /// row operation.
        /// </returns>
        public static Matrix ElementaryRowInterchange(
            this Matrix a, int i, int j)
        {
            // rowI and rowJ are introduced for clarity and to avoid changing a row to itself.
            Vector rowI = a.Row(i);
            Vector rowJ = a.Row(j);
            return a.InsertRow(i, rowJ).InsertRow(j, rowI);
        }
        
        /// <summary>
        /// This function computes the elementary row scaling operation on the
        /// given matrix.
        /// </summary>
        ///
        /// <param name="a">
        /// An N-by-M matrix to perform the elementary row operation on.
        /// </param>
        /// <param name="i">The index of the row to scale.</param>
        /// <param name="c">The value to scale the row by.</param>
        ///
        /// <returns>
        /// The resulting N-by-M matrix after having performed the elementary
        /// row operation.
        /// </returns>
        public static Matrix ElementaryRowScaling(
            this Matrix a, int i, double c)
        {
            return a.InsertRow(i, a.Row(i) * c);
        }


        /// <summary>
        /// This function executes the forward reduction algorithm provided in
        /// the assignment text to achieve row Echelon form of a given
        /// augmented matrix.
        /// </summary>
        ///
        /// <param name="a">
        /// An N-by-M augmented matrix.
        /// </param>
        ///
        /// <returns>
        /// An N-by-M matrix that is the row Echelon form.
        /// </returns>
        public static Matrix ForwardReduction(this Matrix a)
        {
            // If submatrix has zero rows, end of recursion is reached
            if (a.M_Rows - 1 <= 0) return a;
            
            Vector colJ = new Vector(0);
            var pRow = -1;

            // step 1 locate pivot column
            for (var colLoop = 0; colLoop < a.N_Cols; colLoop++)    
            {
                colJ = a.Column(colLoop);
                if (BasicExtensions.IsZeroVector(colJ)) continue;
                
                // step 2 find first non-zero entry
                pRow = BasicExtensions.FirstValueIndex(colJ);
                
                break;
            }

            // if all rows are zero rows end of recursion is reached.
            if (pRow == -1) return a;
            
            
            // step 2 reduction
            a.ElementaryRowInterchange(0, pRow);
            for (var i = 1; i < colJ.Size; i++) 
                a.ElementaryRowReplacement(i, (-colJ[i] / colJ[0]), 0);
            
            // step 3 forming submatrix.
            Vector[] subMatrixVectors = new Vector[a.M_Rows-1];
            for (int i = 1; i < a.M_Rows; i++) 
                subMatrixVectors[i-1] = a.Row(i);
            
            Matrix subMatrix = BasicExtensions.VectorCombine(subMatrixVectors);
           
            // applying steps 1 and 2 on submatrix
            subMatrix = subMatrix.ForwardReduction();
            
            // building return matrix with vectors
            Vector[] retVectors = new Vector[a.M_Rows];
            
            retVectors[0] = a.Row(0);

            for (int i = 1; i < a.M_Rows; i++)
                retVectors[i] = subMatrix.Row(i-1);

            return BasicExtensions.VectorCombine(retVectors);

        }

        /// <summary>
        /// This function executes the backward reduction algorithm provided in
        /// the assignment text given an augmented matrix in row Echelon form.
        /// </summary>
        ///
        /// <param name="a">
        /// An N-by-M augmented matrix in row Echelon form.
        /// </param>
        ///
        /// <returns>
        /// The resulting N-by-M matrix after executing the algorithm.
        /// </returns>
        public static Matrix BackwardReduction(this Matrix a)
        {
            
            Vector pRow = new Vector(0);
            int pRowIndex = -1;
            int uCol = -1;
            
            // Finds pivot row
            for (int i = a.M_Rows-1; i >= 0; i--)
            {
                pRow = a.Row(i);
                if(BasicExtensions.IsZeroVector(pRow)) continue;

                pRowIndex = i;
                uCol = BasicExtensions.FirstValueIndex(pRow);
                break;
            }

            // Scale pivot row to 1
            a.ElementaryRowScaling(pRowIndex, 1.0 / pRow[uCol]);
            
            // Using pivot row to reduce entries to zero 
            for (int i = 0; i < a.M_Rows; i++)
            {
                if(i==pRowIndex) break;
                a.ElementaryRowReplacement(i,-a[i,uCol], pRowIndex);
            }
            
            // If this was the last row to be scaled end of recursion is reached.
            if (a.M_Rows - 1 <= 0) return a;
            
            // step 2 forming submatrix to process other rows. Last row of a is excluded.
            Vector[] subMatrixVectors = new Vector[a.M_Rows-1];
            for (int i = 0; i < subMatrixVectors.Length; i++) 
                subMatrixVectors[i] = a.Row(i);
            
            Matrix subMatrix = BasicExtensions.VectorCombine(subMatrixVectors);

            subMatrix = subMatrix.BackwardReduction();
            
            Vector[] retVectors = new Vector[a.M_Rows];
            
            retVectors[a.M_Rows-1] = a.Row(a.M_Rows-1);

            for (int i = 0; i < a.M_Rows - 1; i++)
                retVectors[i] = subMatrix.Row(i);
            
            return BasicExtensions.VectorCombine(retVectors);
        }

        /// <summary>
        /// This function performs Gauss elimination of a linear system
        /// given in matrix form by a coefficient matrix and a right hand side
        /// vector. It is assumed that the corresponding linear system is
        /// consistent and has exactly one solution.
        /// </summary>
        ///
        /// <remarks>
        /// Hint: Combine ForwardReduction and BackwardReduction.
        /// </remarks>
        ///
        /// <param name="a">An N-by-M matrix.</param>
        /// <param name="b">An N-size vector.</param>
        ///
        /// <returns>The M-sized vector x such that a * x = b.</returns>
        public static Vector GaussElimination(this Matrix a, Vector b)
        {
            return a.AugmentRight(b).ForwardReduction().BackwardReduction().Column(a.N_Cols);
        }
    }

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
        /// Short helper method inserting a row in a matrix.
        /// </summary>
        /// <param name="a">
        /// An N-by-N matrix to perform the row insertion on.
        /// </param>
        /// <param name="i">
        /// The index of the row.
        /// </param>
        /// <param name="rowVec">
        /// The new row as a vector
        /// </param>
        /// <returns>
        /// Matrix with new the row.
        /// </returns>
        public static Matrix InsertRow(this Matrix a, int i, Vector rowVec)
        {
            for (int coloumn = 0; coloumn < a.N_Cols; coloumn++)
                a[i, coloumn] = rowVec[coloumn];
            return a;
        }

        /// <summary>
        /// Returns first value over zero.
        /// </summary>
        /// <param name="a">
        /// Vector to be checked.
        /// </param>
        /// <returns>
        /// index of first zero
        /// </returns>
        /// <remarks>
        /// Only checks if the value is very close to zero, since exact double values are inconsistent.
        /// </remarks>
        public static int FirstValueIndex(Vector a)
        {
            for (int i = 0; i < a.Size; i++)
                if (Math.Abs(a[i]) > 1e-5f) return i;
            return -1;
        }

        /// <summary>
        /// Combines vector list to matrix. Each vector becomes a row in the matrix.
        /// </summary>
        /// <param name="vecArr">
        /// Vectors to become matrix
        /// </param>
        /// <returns>
        /// Matrix consisting of vector list
        /// </returns>
        public static Matrix VectorCombine(Vector[] vecArr)
        {
            var rowCount = vecArr.Length;
            var colCount = rowCount > 0 ? vecArr[0].Size : 0;
            var retDoubleArr = new double[rowCount, vecArr[0].Size];
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < colCount; j++)
                    retDoubleArr[i, j] = vecArr[i][j];
            
            return new Matrix(retDoubleArr);    
        }

        /// <summary>
        /// Checks if vector is zero vector
        /// </summary>
        /// <param name="vec">
        /// Vector to be checked
        /// </param>
        /// <returns>
        /// True if vector is a zero vector else false
        /// </returns>
        public static bool IsZeroVector(Vector vec)
        {
            for (int i = 0; i < vec.Size; i++)
                if (Math.Abs(vec[i]) >= 1e-5f) return false;
            return true;
        }
    }
}
