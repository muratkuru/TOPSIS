using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TOPSIS.Process;

namespace TOPSIS.Test
{
    [TestClass]
    public class CalculationTest
    {
        private Calculation calculation;
        private double[,] decisionMatrix;
        private double[] weights;
        private double[,] correctNormalizedMatrix;
        private double[,] calculatedNormalizedMatrix;
        private double[,] correctNormalizedWeightedMatrix;
        private double[,] calculatedNormalizedWeightedMatrix;
        private double[] correctIdealSolution;
        private double[] calculatedIdealSolution;
        private double[] correctNegativeIdealSolution;
        private double[] correctIdealSolutionSeparation;
        private double[] correctNegativeIdealSolutionSeparation;
        private double[] calculatedNegativeIdealSolution;
        private double[] calculatedIdealSolutionSeparation;
        private double[] calculatedNegativeIdealSolutionSeparation;
        private double[] correctRelativeClosenessToIdealSolution;
        private double[] calculatedRelativeClosenessToIdealSolution;
        private int targetCorrectitude;

        [TestInitialize]
        public void Initialize()
        {
            decisionMatrix = new double[3, 4] 
            { 
                { 15, 40, 25, 40 }, 
                { 20, 30, 20, 35 }, 
                { 30, 10, 30, 15 }
            };
            weights = new double[4] { 0.3, 0.1, 0.4, 0.2 };

            correctNormalizedMatrix = new double[3, 4]
            {
                { 0.384, 0.784, 0.570, 0.724 },
                { 0.512, 0.588, 0.456, 0.634 },
                { 0.768, 0.196, 0.684, 0.272 }
            };
            correctNormalizedWeightedMatrix = new double[3, 4]
            {
                { 0.115, 0.078, 0.228, 0.145 },
                { 0.154, 0.059, 0.182, 0.127 },
                { 0.230, 0.020, 0.274, 0.054 }
            };
            correctIdealSolution = new double[] { 0.230, 0.078, 0.274, 0.145 };
            correctNegativeIdealSolution = new double[] { 0.115, 0.020, 0.182, 0.054 };
            correctIdealSolutionSeparation = new double[] { 0.124, 0.122, 0.108 };
            correctNegativeIdealSolutionSeparation = new double[] { 0.117, 0.091, 0.147 };
            correctRelativeClosenessToIdealSolution = new double[] { 0.486, 0.427, 0.576 };

            calculation = new Calculation(decisionMatrix, weights);

            calculatedNormalizedMatrix = calculation.NormalizedMatrix;
            calculatedNormalizedWeightedMatrix = calculation.NormalizedWeightedMatrix;
            calculatedIdealSolution = calculation.IdealSolution;
            calculatedNegativeIdealSolution = calculation.NegativeIdealSolution;
            calculatedIdealSolutionSeparation = calculation.IdealSolutionSeparation;
            calculatedNegativeIdealSolutionSeparation = calculation.NegativeIdealSolutionSeparation;
            calculatedRelativeClosenessToIdealSolution = calculation.RelativeClosenessToIdealSolution;

            targetCorrectitude = calculation.RowCount * calculation.ColumnCount;
        }

        [TestMethod]
        public void IsNormalizedMatrixCorrect()
        {
            int totalCorrectitude = 0;

            for(int i = 0; i < calculation.RowCount; i++)
            {
                for (int j = 0; j < calculation.ColumnCount; j++)
                {
                    double roundedResult = Math.Round(calculatedNormalizedMatrix[i, j], 3);
                    if (roundedResult == correctNormalizedMatrix[i, j])
                        totalCorrectitude++;
                }
            }

            Assert.AreEqual(targetCorrectitude, totalCorrectitude);
        }

        [TestMethod]
        public void IsNormalizedWeightedMatrixCorrect()
        {
            int totalCorrectitude = 0;

            for(int i = 0; i < calculation.RowCount; i++)
            {
                for(int j = 0; j < calculation.ColumnCount; j++)
                {
                    double roundedResult = Math.Round(calculatedNormalizedWeightedMatrix[i, j], 3);
                    if (roundedResult == correctNormalizedWeightedMatrix[i, j])
                        totalCorrectitude++;
                }
            }

            Assert.AreEqual(targetCorrectitude, totalCorrectitude);
        }

        [TestMethod]
        public void IsIdealSolutionCorrect()
        {
            int totalCorrectitude = 0;

            for(int i = 0; i < calculation.ColumnCount; i++)
            {
                double roundedResult = Math.Round(calculatedIdealSolution[i], 3);
                if (roundedResult == correctIdealSolution[i])
                    totalCorrectitude++;
            }

            Assert.AreEqual(calculation.ColumnCount, totalCorrectitude);
        }

        [TestMethod]
        public void IsNegativeIdealSolutionCorrect()
        {
            int totalCorrectitude = 0;

            for (int i = 0; i < calculation.ColumnCount; i++)
            {
                double roundedResult = Math.Round(calculatedNegativeIdealSolution[i], 3);
                if (roundedResult == correctNegativeIdealSolution[i])
                    totalCorrectitude++;
            }

            Assert.AreEqual(calculation.ColumnCount, totalCorrectitude);
        }

        [TestMethod]
        public void IsIdealSolutionSeparationCorrect()
        {
            int totalCorrectitude = 0;

            for(int i = 0; i < calculation.RowCount; i++)
            {
                double roundedResult = Math.Round(calculatedIdealSolutionSeparation[i], 3);
                if (roundedResult == correctIdealSolutionSeparation[i])
                    totalCorrectitude++;
            }

            Assert.AreEqual(calculation.RowCount, totalCorrectitude);
        }

        [TestMethod]
        public void IsNegativeIdealSolutionSeparationCorrect()
        {
            int totalCorrectitude = 0;

            for (int i = 0; i < calculation.RowCount; i++)
            {
                double roundedResult = Math.Round(calculatedNegativeIdealSolutionSeparation[i], 3);
                if (roundedResult == correctNegativeIdealSolutionSeparation[i])
                    totalCorrectitude++;
            }

            Assert.AreEqual(calculation.RowCount, totalCorrectitude);
        }

        [TestMethod]
        public void IsRelativeClosenessToIdealSolutionCorrect()
        {
            int totalCorrectitude = 0;

            for(int i = 0; i < calculation.RowCount; i++)
            {
                double roundedResult = Math.Round(calculatedRelativeClosenessToIdealSolution[i], 3);
                if (roundedResult == correctRelativeClosenessToIdealSolution[i])
                    totalCorrectitude++;
            }

            Assert.AreEqual(calculation.RowCount, totalCorrectitude);
        }
    }
}
