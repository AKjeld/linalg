using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    /// <summary>
    /// An M-by-N matrix.
    /// </summary>
    ///
    /// <remarks>
    /// This implement is "row-major". This means that it is likely to be
    /// faster to deal in rows rather than columns. For instance, if you must
    /// iterate over all the elements, iterate over rows in the outer loop,
    /// and columns in the inner loop.
    /// </remarks>
    public class Matrix
    {
        private readonly double[,] _xs;

        /// <summary>
        /// Initializes an m-by-n Matrix with all 0's.
        /// </summary>
        public Matrix(int mRows, int nColumns)
        {
            _xs = new double[mRows, nColumns];
        }

        public Matrix(double[,] xs)
        {
            _xs = (double[,])xs.Clone();
        }

        public static implicit operator Matrix(double[,] xs)
        {
            return new Matrix(xs);
        }

        public double[,] ToArray()
        {
            return (double[,])_xs.Clone();
        }

        public int M_Rows => _xs.GetLength(0);

        public int N_Cols => _xs.GetLength(1);

        public Tuple<int, int> Size => Tuple.Create(M_Rows, N_Cols);

        public double this[int i, int j]
        {
            get
            {
                return _xs[i, j];
            }
            set
            {
                _xs[i, j] = value;
            }
        }

        /// <summary>
        /// Get a particular row vector.
        /// </summary>
        public Vector Row(int i)
        {
            var nCols = N_Cols;
            var retval = new Vector(nCols);
            for (var j = 0; j < nCols; j++)
            {
                retval[j] = _xs[i, j];
            }
            return retval;
        }

        /// <summary>
        /// Get a particular column vector.
        /// </summary>
        ///
        /// <remarks>
        /// The implementation is row-major, so this is expected to be a slow
        /// endeavour.
        /// </remarks>
        public Vector Column(int j)
        {
            var mRows = M_Rows;
            var retval = new Vector(mRows);
            for (var i = 0; i < mRows; i++)
            {
                retval[i] = _xs[i, j];
            }
            return retval;
        }

        private IEnumerable<IEnumerable<string>> RightFixatedStrings()
        {
            if (_xs.Length == 0)
            {
                return new List<List<string>> { new List<string>() };
            }
            else
            {
                var ss = new string[_xs.Length];
                var ls = new int[_xs.Length];
                var k = 0;
                foreach (var x in _xs)
                {
                    ss[k] = x.ToString();
                    ls[k] = ss[k].Length;
                    k++;
                }

                var maxLength = ls.Max();
                var format = "{0," + maxLength.ToString() + "}";

                var mRows = M_Rows;
                var nCols = N_Cols;
                var retval = new List<List<string>>(mRows);

                k = 0;
                for (var i = 0; i < mRows; i++)
                {
                    var row = new List<string>(nCols);
                    for (var j = 0; j < nCols; j++)
                    {
                        row.Add(string.Format(format, ss[k]));
                        k++;
                    }
                    retval.Add(row);
                }

                return retval;
            }
        }

        public override string ToString()
        {
            var xs = RightFixatedStrings();
            var builder = new StringBuilder();
            foreach (var row in xs)
            {
                builder.Append(
                    $"| {string.Join(" ", row)} |\n");
            }
            return builder.ToString();
        }

        public override bool Equals(object obj)
        {
            // One does not simply compare floats for equality.
            throw new InvalidOperationException();
        }

        public override int GetHashCode()
        {
            return _xs.GetHashCode();
        }
    }
}