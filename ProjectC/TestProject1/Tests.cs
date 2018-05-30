using System;
using Core;
using NUnit.Framework;
using ProjectC;


namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Matrix testMatrix = new Matrix(1,1);
            testMatrix[0, 0] = 2.0;
            Assert.True(AdvancedExtensions.Determinant(testMatrix)==2.0);
        }

        [Test]
        public void Test2()
        {
            Matrix testMatrix = new Matrix(2, 2);
            testMatrix[0, 0] = 1.0;
            testMatrix[0, 1] = 2.0;
            testMatrix[1, 0] = 3.0;
            testMatrix[1, 1] = 4.0;

            double determinant = AdvancedExtensions.Determinant(testMatrix);

            Assert.True(determinant == -2.0, determinant.ToString());
        }

        [Test]
        public void Test3()
        {
            Matrix testMatrix = new Matrix(4,4);
            testMatrix[0, 0] = 1.0;
            testMatrix[0, 1] = 3.0;
            testMatrix[0, 2] = 5.0;
            testMatrix[0, 3] = 9.0;
            
            testMatrix[1, 0] = 1.0;
            testMatrix[1, 1] = 3.0;
            testMatrix[1, 2] = 1.0;
            testMatrix[1, 3] = 7.0;
            
            testMatrix[2, 0] = 4.0;
            testMatrix[2, 1] = 3.0;
            testMatrix[2, 2] = 9.0;
            testMatrix[2, 3] = 7.0;
            
            testMatrix[3, 0] = 5.0;
            testMatrix[3, 1] = 2.0;
            testMatrix[3, 2] = 0.0;
            testMatrix[3, 3] = 9.0;
            
            double determinant = AdvancedExtensions.Determinant(testMatrix);

            Assert.True(determinant == -376.0, determinant.ToString());
        }
    }
}