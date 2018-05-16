using System;
using System.Linq;

namespace TOPSIS.Process
{
    public class Calculation
    {
        private double[,] decisionMatrix;
        private double[] weights;

        public Calculation(double[,] decisionMatrix, double[] weights)
        {
            this.decisionMatrix = decisionMatrix;
            this.weights = weights;

            RowCount = decisionMatrix.GetLength(0);
            ColumnCount = decisionMatrix.GetLength(1);

            NormalizedMatrix = new double[RowCount, ColumnCount];
            NormalizedWeightedMatrix = new double[RowCount, ColumnCount];

            IdealSolution = new double[ColumnCount];
            NegativeIdealSolution = new double[ColumnCount];

            IdealSolutionSeparation = new double[RowCount];
            NegativeIdealSolutionSeparation = new double[RowCount];

            RelativeClosenessToIdealSolution = new double[RowCount];

            SetNormalizedMatrix();
            SetNormalizedWeightedMatrix();
            SetIdealSolutions();
            SetIdealSolutionSeparations();
            SetRelativeClosenessToIdealSolution();
        }

        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public double[,] NormalizedMatrix { get; set; }

        public double[,] NormalizedWeightedMatrix { get; set; }

        public double[] IdealSolution { get; set; }

        public double[] NegativeIdealSolution { get; set; }

        public double[] IdealSolutionSeparation { get; set; }

        public double[] NegativeIdealSolutionSeparation { get; set; }

        public double[] RelativeClosenessToIdealSolution { get; set; }

        private void SetNormalizedMatrix()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    double rowSum = 0;
                    for(int k = 0; k < RowCount; k++)
                        rowSum += Math.Pow(decisionMatrix[k, j], 2);

                    NormalizedMatrix[i, j] = decisionMatrix[i, j] / Math.Sqrt(rowSum);
                }
            }
        }

        private void SetNormalizedWeightedMatrix()
        {
            for(int i = 0; i < RowCount; i++)
                for(int j = 0; j < ColumnCount; j++)
                    NormalizedWeightedMatrix[i, j] = NormalizedMatrix[i, j] * weights[j];
        }

        private void SetIdealSolutions()
        {
            for(int i = 0; i < ColumnCount; i++)
            {
                double[] column = new double[RowCount];
                for(int j = 0; j < RowCount; j++)
                {
                    column[j] = NormalizedWeightedMatrix[j, i];
                }
                IdealSolution[i] = column.Max();
                NegativeIdealSolution[i] = column.Min();
            }
        }

        private void SetIdealSolutionSeparations()
        {
            for(int i = 0; i < RowCount; i++)
            {
                double totalIdeal = 0;
                double totalNegativeIdeal = 0;
                for(int j = 0; j < ColumnCount; j++)
                {
                    totalIdeal += Math.Pow((NormalizedWeightedMatrix[i, j] - IdealSolution[j]), 2);
                    totalNegativeIdeal += Math.Pow((NormalizedWeightedMatrix[i, j] - NegativeIdealSolution[j]), 2);
                }
                IdealSolutionSeparation[i] = Math.Sqrt(totalIdeal);
                NegativeIdealSolutionSeparation[i] = Math.Sqrt(totalNegativeIdeal);
            }
        }

        private void SetRelativeClosenessToIdealSolution()
        {
            for(int i = 0; i < RowCount; i++)
                RelativeClosenessToIdealSolution[i] = NegativeIdealSolutionSeparation[i] / (IdealSolutionSeparation[i] + NegativeIdealSolutionSeparation[i]);
        }
    }
}
