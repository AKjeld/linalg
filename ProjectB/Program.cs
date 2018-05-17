using System;
using System.Collections.Generic;
using Core;


namespace ProjectB
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


        private static bool TestRowScaling(Matrix A, int i, double f, Matrix Expected)
        {
            // A is the original matrix, i the index of the line to be scaled, 
            // f the scaling factor and Expected the expected result.

            Matrix Av = null;
            var status = true;
            const string taskName = "Matrix.ElementaryRowScaling()";
            
            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                Av = A.ElementaryRowScaling(i, f);
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
        
        private static bool TestRowInterchange(Matrix A, int i, int j, Matrix Expected)
        {
            // A is the original matrix, i and j the indices of the lines to be swapped, 
            // and Expected the expected result.

            Matrix Av = null;
            var status = true;
            const string taskName = "Matrix.ElementaryRowInterchange()";
            
            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                Av = A.ElementaryRowInterchange(i, j);
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
        
        private static bool TestRowReplacement(Matrix A, int i, double f, int j, Matrix Expected)
        {
            // A is the original matrix, i the index of the line to be modified, 
            // f the scaling factor or line j, and Expected the expected result.

            Matrix Av = null;
            var status = true;
            const string taskName = "Matrix.ElementaryRowReplacement()";
            
            PrintUnderLined($"Tests for the {taskName} method.");

            try
            {
                Av = A.ElementaryRowReplacement(i, f, j);
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
        
        
        private static bool TestForwardReduction(string subtask, Matrix A, Matrix Expected)
        {
            // A is the original matrix and Expected the expected result.

            Matrix Av = null;
            var status = true;
            const string taskName = "Matrix.ForwardReduction()";
            
            PrintUnderLined($"({subtask}) tests for the {taskName} method.");

            try
            {
                Av = A.ForwardReduction();
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
            PrintUnderLined($"({subtask}) end of test for the {taskName} method.", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }
        
        
        private static bool TestBackwardReduction(string subtask, Matrix A, Matrix Expected)
        {
            // A is the original matrix and Expected the expected result.

            Matrix Av = null;
            var status = true;
            const string taskName = "Matrix.BackwardReduction()";
            
            PrintUnderLined($"({subtask}) tests for the {taskName} method.");

            try
            {
                Av = A.BackwardReduction();
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
            PrintUnderLined($"({subtask}) end of test for the {taskName} method.", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }
        
        
        private static bool TestGaussElimination(Matrix A, Vector V, Vector Expected)
        {
            // A is the system matrxi, V the second member and Expected the expected vector solution.

            Vector Av = null;
            var status = true;
            const string taskName = "Matrix.GaussElimination()";
            
            PrintUnderLined($"tests for the {taskName} method.");

            try
            {
                Av = A.GaussElimination(V);
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
            PrintUnderLined($"End of test for the {taskName} method.", line: '-');
            Console.WriteLine("\n\n");
            return status;
        }
    }
}