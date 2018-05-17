using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Vector
    {
        private readonly double[] _xs;

        /// <summary>
        /// Initializes an n-size vector with all 0's.
        /// </summary>
        public Vector(int n)
        {
            _xs = new double[n];
        }

        public Vector(double[] xs)
        {
            _xs = (double[])xs.Clone();
        }

        public static implicit operator Vector(double[] xs)
        {
            return new Vector(xs);
        }

        public double[] ToArray()
        {
            return (double[])_xs.Clone();
        }

        public int Size => _xs.Length;

        public double this[int i]
        {
            get
            {
                return _xs[i];
            }
            set
            {
                _xs[i] = value;
            }
        }

        public static Vector operator*(Vector v, double y)
        {
            var size = v.Size;
            var retval = new Vector(v._xs);
            for (var i = 0; i < size; i++)
            {
                retval[i] *= y;
            }
            return retval;
        }

        public static Vector operator*(double x, Vector v)
        {
            var size = v.Size;
            var retval = new Vector(v._xs);
            for (var i = 0; i < size; i++)
            {
                retval[i] *= x;
            }
            return retval;
        }

        public static Vector operator+(Vector xs, Vector ys)
        {
            var size = Math.Min(xs.Size, ys.Size);
            var retval = new Vector(size);
            for (var i = 0; i < size; i++)
            {
                retval[i] = xs[i] + ys[i];
            }
            return retval;
        }

        public static Vector operator-(Vector xs, Vector ys)
        {
            var size = Math.Min(xs.Size, ys.Size);
            var retval = new Vector(size);
            for (var i = 0; i < size; i++)
            {
                retval[i] = xs[i] - ys[i];
            }
            return retval;
        }

        public static double operator*(Vector xs, Vector ys)
        {
            var size = Math.Min(xs.Size, ys.Size);
            var retval = 0.0;
            for (var i = 0; i < size; i++)
            {
                retval += xs[i] * ys[i];
            }
            return retval;
        }

        private IEnumerable<string> RightFixatedStrings()
        {
            if (_xs.Length == 0)
            {
                return new string[] { };
            }
            else
            {
                var xs = _xs.Select((x) => x.ToString());
                var maxLength = xs.Select((x) => x.Length).Max();
                var format = "{0," + maxLength.ToString() + "}";
                return xs.Select((x) => String.Format(format, x));
            }
        }

        public override string ToString()
        {
            var xs = RightFixatedStrings();
            return $"| {String.Join(" ", xs)} |\n";
        }

        public override bool Equals(object obj)
        {
            var vector = obj as Vector;
            return vector != null && _xs.SequenceEqual(vector._xs);
        }

        public override int GetHashCode()
        {
            return _xs.GetHashCode();
        }
    }
}