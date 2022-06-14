using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accord.Math.Optimization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using BalaceController.Models;

namespace BalaceController.Services

{
    public class BalanceService
    {
        [Produces("application/json")]
        public BalanceOutput balance(BalanceInput balanceInput)
        {
            int count = balanceInput.arrowInput.Count;
            BalanceOutput result = new BalanceOutput();

            //сгенерировали матрицу W
            var W = Matrix<double>.Build.DenseDiagonal(count, count, 1);
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                        W[i, j] = 1 / (balanceInput.arrowInput[i].Tolerance * balanceInput.arrowInput[i].Tolerance);
                }
            }
            
            //сгененрировали единичную матрицу I
            var I = Matrix<double>.Build.DenseDiagonal(count, count, 1);

            //сгенерировали матрицу значений потоков
            double[] x0 = new double[count];
            for (int i = 0; i < count; i++)
            {
                x0[i] = balanceInput.arrowInput[i].Measured;
            }

            //сгенерировали матрицу b
            double[] b = new double[3 + count * 2]; // 3 - число условий g кол-ву узлов
                                                    // count - число условий по кол-ву точек  * 2
            for (int j = 0; j < 3; j++)
            {
                b[j] = 0;
            }
            
            //установили исключение для боксовых ограничений
            result.exeption = new List<string>();
            for (int j = 0; j < count * 2; j += 2)
            {
                b[j + 3] = balanceInput.arrowInput[j / 2].Min;
                b[j + 4] = -balanceInput.arrowInput[j / 2].Max;
                if (balanceInput.arrowInput[j / 2].Min >= balanceInput.arrowInput[j / 2].Max)
                    result.exeption.Add("Некорректные данные в потоке " + (j / 2 + 1)); //Надо сделать ошибки через ошибку 500
            }

            //сгенерировали матрицу А
            double[,] A = new double[3 + count * 2, count];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i + 1 == balanceInput.arrowInput[j].SourceId)
                    {
                        A[i, j] = -1;
                    }
                    else if (i + 1 == balanceInput.arrowInput[j].DestinationId)
                    {
                        A[i, j] = 1;
                    }
                    else
                    {
                        A[i, j] = 0;
                    }
                }
            }
            int k = 3;
            for (int i = 0; i < count; i++)
            {
                A[k, i] = 1;
                A[k + 1, i] = -1;
                k += 2;
            }

            int numberOfEqualities = 3;//количество узлов
            double[,] H = I.Multiply(W).ToArray();
            Matrix<double> Mh = -Matrix<double>.Build.DenseOfArray(H);
            double[] d = Mh.Multiply(DenseVector.Build.DenseOfArray(x0)).ToArray();
            var solver = new GoldfarbIdnani(H, d, A, b, numberOfEqualities);
            solver.Minimize();

            result.arrowOutput = new List<ArrowOutput>();
            result.Status = solver.Status.ToString();
            if (result.Status == "Success")
            {
                double[] solution = solver.Solution;
                for (int i = 0; i < solution.Length; i++)
                {
                    ArrowOutput arrowOutput = new ArrowOutput();
                    arrowOutput.Id = balanceInput.arrowInput[i].Id;
                    arrowOutput.Name = balanceInput.arrowInput[i].Name;
                    arrowOutput.ReconcliedValue = solution[i];
                    result.arrowOutput.Add(arrowOutput);
                }
            }
            return result;
        }

        /*public BalanceOutput Calculate2(double[,] A, double[] b, int numberOfEqualities, double[] x0, double[,] I, double[,] W)
        {

            Matrix<double> Mi = Matrix<double>.Build.DenseOfArray(I);
            Matrix<double> Mw = Matrix<double>.Build.DenseOfArray(W);

            double[,] H = Mi.Multiply(Mw).ToArray();
            Matrix<double> Mh = -Matrix<double>.Build.DenseOfArray(H);

            double[] d = Mh.Multiply(DenseVector.Build.DenseOfArray(x0)).ToArray();
            //int numberOfEqualities = 3;

            //var solver = new GoldfarbIdnani(H, d, A, b, numberOfEqualities);

            // solver.Minimize();
            //double[] solution = solver.Solution;
            
            



            var constraints = new List<LinearConstraint>()
            {
                //БОКСОВЫЕ ОГРАНИЧЕНИЯ
                // x>=LowBorder, т.е x>=0
                new LinearConstraint(numberOfVariables: 7)//всего 7 аргументов, т.е x0>=0,...,x6>=0
                {
                    VariablesAtIndices = new[] { 0, 1, 2, 3, 4, 5, 6 }, // index xn
                    ShouldBe = ConstraintType.GreaterThanOrEqualTo,//тип ограничения >=
                    Value = 0//значение LowBorder
                },
                // x<=UpBorder, т.е x<=10000
                new LinearConstraint(numberOfVariables: 7)//всего 7 аргументов, т.е x0<=10000,...,x6<=10000
                {
                    VariablesAtIndices = new[] { 0, 1, 2, 3, 4, 5, 6 }, // index xn
                    ShouldBe = ConstraintType.LesserThanOrEqualTo,//тип ограничения <=
                    Value = 10000//значение LowBorder
                },

                //ОГРАНИЧЕНИЯ ТИПА Ax=0

                new LinearConstraint(numberOfVariables: 7)//количество аргументов
                {

                   VariablesAtIndices = new int[] {0, 1, 2, 3, 4, 5, 6},
                    CombinedAs = new double[] { 1, -1, -1, 0, 0, 0, 0 },//значение одной строчки матрицы А
                    ShouldBe = ConstraintType.EqualTo,
                    Value = 0//значение одной строчки столбца Б
                },

                // Define the second constraint, which involves x and y
                new LinearConstraint(numberOfVariables: 7)
                {
                  
                    VariablesAtIndices = new int[] {0, 1, 2, 3, 4, 5, 6}, // index 0 (x) and index 1 (y)
                    CombinedAs = new double[] { 0, 0, 1, -1, -1, 0, 0 }, // when combined as x - y
                    ShouldBe = ConstraintType.EqualTo,
                    Value = 0
                },
                new LinearConstraint(numberOfVariables: 7)
                {
                   
                    VariablesAtIndices = new int[] { 0, 1, 2, 3, 4, 5, 6}, // index 0 (x) and index 1 (y)
                    CombinedAs = new double[] { 0, 0, 0, 0, 1, -1, -1 }, // when combined as x - y
                    ShouldBe = ConstraintType.EqualTo,
                    Value = 0
                }
            };
            // Now we can finally create our optimization problem
           var solver = new GoldfarbIdnani(
                function: new QuadraticObjectiveFunction(H, d),
                constraints: constraints);
            //var solver = new GoldfarbIdnani(H, d, A, b, numberOfEqualities);


            // And attempt solve for the min:
            bool success = solver.Minimize();

            // The solution was { 10, 5 }
            double[] solution = solver.Solution;

            // With the minimum value 170.0
            double minValue = solver.Value;
            

            return new BalanceOutput() { Answer = solution, Info = "Calculated" };
        }*/
    }
}
