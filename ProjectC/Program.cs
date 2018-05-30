using System;
using Core;
using System.Collections.Generic;
namespace ProjectC
{
    public partial class MainClass
    {
        public static void Main(string[] args)
        {
            InitAllLists();
            RunTests();
            PrintReport();
            Console.WriteLine("\n\n");
        }
        
        // Print a summary of successful runs for a given test task
        private static int PrintSummary(List<bool> results, string task, int padd = 75, string prefix = "Success for ")
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


        
        private static readonly double Tolerance = 1e-4;


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
            return (sum < Tolerance) && !Double.IsNaN(sum);

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
            return (sum < Tolerance) && !Double.IsNaN(sum);
        }

        // display a message followed by [PASSED] or [FAILED]
        private static void OutMessage(string taskName, string subTaskName, bool status)
        {
            var s = $"{taskName} {subTaskName}:".PadRight(60,' ');
            var res = status ? "[PASSED]" : "[FAILED]";
            Console.WriteLine($"{s} {res}");
        }


        // Print some text underlined-
        private static void PrintUnderLined(string text, char line = '=')
        {
            Console.WriteLine(text);
            Console.WriteLine("".PadRight(text.Length, line));
        }

        private static bool TestSquareSubMatrix(Matrix A, int i, int j, Matrix Expected)
        {
            // A is a square matrix, i and j indices of line and columns
            // to be removed and Expected the expected resulting matrix

            Matrix Av = null;
            var status = true;
            const string taskName = "Matrix.SquareSubMatrix()";
            
            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                Av = A.SquareSubMatrix(i, j);
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
        
        private static bool TestDeterminant(Matrix A, double Expected)
        {
            // A is a square matrix Expected the expected determinant

            var det = 0.0;
            var status = true;
            const string taskName = "Matrix.Determinant()";
            
            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                det = A.Determinant();
            }
            catch
            {
                OutMessage(taskName, "Run", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Run", true);

            if ((Math.Abs(det - Expected) > Tolerance) || Double.IsNaN(det))
            {
                OutMessage(taskName, "Value", false);
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
       
        
        
        private static bool TestGramSchmidt(Matrix A, Matrix Qexpected, Matrix Rexpected)
        {
            // A is a (m, n) matrix with independent columns, Qexpected and Rexpected the 
            // expected matrices of the QR decomposition from the Gram-Schmidt algorithm.
            
            Tuple<Matrix, Matrix> QR = null;
            var status = true;
            const string taskName = "Matrix.GramSchmidt()";
            
            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                QR = A.GramSchmidt();
            }
            catch
            {
                OutMessage(taskName, "Run", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Run", true);
            var Q = QR.Item1;
            var R = QR.Item2;
            
            if (!CompareMatrixDimensions(Q, Qexpected) || !CompareMatrixDimensions(R, Rexpected))
            {
                OutMessage(taskName, "Dims", false);
                status = false;
                goto end_of_test;
            }

            OutMessage(taskName, "Dims", true);
            if (!CompareMatrices(Q, Qexpected) || !CompareMatrices(R, Rexpected))
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
