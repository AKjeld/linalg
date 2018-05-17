using System;

namespace Core
{
    public static class VectorFactory
    {
        /// <summary>Create a vector of n ones.</summary>
        public static Vector Ones(int n)
        {
            var retval = new Vector(n);
            for (var i = 0; i < n; i++)
            {
                retval[i] = 1.0;
            }
            return retval;
        }
    }
}