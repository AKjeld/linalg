using System;
using System.Collections.Generic;
using Core;

namespace ProjectA
{
    public static partial class MainClass
    {

        // Runs the test suite for the data generated in MainClass.cs
        public static void Main(string[] args)
        {
            InitAllLists();
            RunTests();
            PrintReport();
            Console.WriteLine("\n\n");
        }


        // Print a summary of successful runs for a given test task
        private static int PrintSummary(List<bool> results, string task, int padd = 65, string prefix = "Success for ")
        {
            var total = results.Count;
            var passed = 0;
            foreach (var status in results)
            {
                if (status) passed++;
            }

            var s = $"{prefix}{task} method:".PadRight(padd, ' ');
            var t = $"{s} {passed}/{total} runs";
            if (prefix.Length > 0)
                Console.WriteLine("".PadRight(t.Length, '='));
            Console.WriteLine(t);
            return t.Length;
        }


        private static readonly double Tolerance = 1e-3;


        // For legibility
        private static bool CompareVectorDimensions(Vector v, Vector w)
        {
            return v.Size == w.Size;
        }


        // compute L1-distance between arguments. If less than
        // Tolerance, they are considered equal.
        // v and w must have same size
        private static bool CompareVectors(Vector v, Vector w)
        {
            var sum = 0.0;
            for (var i = 0; i < v.Size; ++i)
                sum += Math.Abs(v[i] - w[i]);
            sum /= v.Size;
            return sum < Tolerance;

        }

        // For legibility.
        private static bool CompareMatrixDimensions(Matrix A, Matrix B)
        {
            return (A.M_Rows == B.M_Rows) && (A.N_Cols == B.N_Cols);
        }


        // compute L1-distance between arguments. If less than
        // Tolerance, they are considered equal.
        // A and B must have same dimensions
        private static bool CompareMatrices(Matrix A, Matrix B)
        {
            var sum = 0.0;
            for (var i = 0; i < A.M_Rows; ++i)
            {
                for (var j = 0; j < A.N_Cols; ++j)
                {
                    sum += Math.Abs(A[i, j] - B[i, j]);
                }
            }

            sum /= (A.M_Rows * A.N_Cols);
            return sum < Tolerance;
        }

        // display a message followed by [PASSED] or [FAILED]
        private static void OutMessage(string taskName, string subTaskName, bool status)
        {
            var s = $"{taskName} {subTaskName}:".PadRight(50,' ');
            var res = status ? "[PASSED]" : "[FAILED]";
            Console.WriteLine($"{s} {res}");
        }


        // Print some text underlined-
        private static void PrintUnderLined(string text, char line = '=')
        {
            Console.WriteLine(text);
            Console.WriteLine("".PadRight(text.Length, line));
        }

        // All the tests have the same structure.
        // *) check that the implemented method runs
        // *) if applicable, check that the result has expected size/dimensions
        // *) check that the result has the expected value(s)
        // Only if all the tests are successful, the method returns tru

        private static bool TestMatrixAugmentation(Matrix A, Vector v, Matrix Expected)
        {
            const string taskName = "Matrix.AugmentRight(Vector)";
            Matrix Av = null;

            var status = true;

            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                Av = A.AugmentRight(v);
            }
            catch
            {
                OutMessage(taskName, "Run", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Run", true);

            if (!CompareMatrixDimensions(Av, Expected))
            {
                OutMessage(taskName, "Dims", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Dims", true);
            if (!CompareMatrices(Av, Expected))
            {
                OutMessage(taskName, "Values", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Values", true);
            OutMessage(taskName, "All", true);

        end_of_test:
            PrintUnderLined($"End of test for the {taskName} method.", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }


        private static bool TestMatrixVectorProduct(string MatType, Matrix A, Vector v, Vector Expected)
        {
            var taskName = "Matrix.Product(Vector)";
            Vector Av = null;

            var status = true;

            PrintUnderLined($"{MatType}, Tests for the {taskName} method.");
            try
            {
                Av = A.Product(v);
            }
            catch
            {
                OutMessage(taskName, "Run", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Run", true);

            if (!CompareVectorDimensions(Av, Expected))
            {
                OutMessage(taskName, "Dims", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Dims", true);

            if (!CompareVectors(Av, Expected))
            {
                OutMessage(taskName, "Values", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Values", true);
            OutMessage(taskName, "All", true);

         end_of_test:
            PrintUnderLined($"{MatType}, end of test for the {taskName} method.", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }



        private static bool TestMatrixMatrixProduct(string MatType, Matrix A, Matrix B, Matrix Expected)
        {
            var taskName = "Matrix.Product(Matrix)";
            Matrix ML = null;

            var status = true;

            PrintUnderLined($"{MatType}, Tests for the {taskName} method.");
            try
            {
                ML = A.Product(B);
            }
            catch
            {
                OutMessage(taskName, "Run", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Run", true);

            if (!CompareMatrixDimensions(ML, Expected))
            {
                OutMessage(taskName, "Dims", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Dims", true);

            if (!CompareMatrices(ML, Expected))
            {
                OutMessage(taskName, "Value", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Value",true);
            OutMessage(taskName, "All", true);

        end_of_test:
            PrintUnderLined($"{MatType}, Tests for the {taskName} method.", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }





        private static bool TestTranspose(string MatType, Matrix A, Matrix Expected)
        {
            var taskName = $"Matrix.Transpose()";
            Matrix MT = null;

            var status = true;

            PrintUnderLined($"{MatType}, Tests for the {taskName} method.");

            try
            {
                MT = A.Transpose();
            }
            catch
            {
                OutMessage(taskName, "Run", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Run", true);

            if (!CompareMatrixDimensions(MT, Expected))
            {
                OutMessage(taskName, "Dims", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Dims", true);

            if (!CompareMatrices(MT, Expected))
            {
                OutMessage(taskName, "Values", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Values", true);
            OutMessage(taskName, "All", true);

        end_of_test:
            PrintUnderLined($"{MatType}, end of test for the {taskName} method", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }



        private static bool TestVectorNorm(Vector v, double Expected)
        {
            const string taskName = "Vector.VectorNorm()";
            var nv = 0.0;

            var status = true;

            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                nv = v.VectorNorm();
            }
            catch
            {
                OutMessage(taskName, "Run", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Run", true);

            if (Math.Abs(nv- Expected) > Tolerance)
            {
                OutMessage(taskName, "Values", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Values", true);
            OutMessage(taskName, "All", true);

        end_of_test:
            PrintUnderLined($"End of test for the {taskName} method.", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }
    }
}
