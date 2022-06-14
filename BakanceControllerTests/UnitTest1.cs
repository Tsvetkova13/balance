using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BalanceController;
using MathNet.Numerics.LinearAlgebra;

namespace BakanceControllerTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestOne()
        {
            double[,] A ={
                            { 1, -1, -1, 0, 0, 0, 0 },
                            { 0, 0, 1, -1, -1, 0, 0 },
                            { 0, 0, 0, 0, 1, -1, -1 },
                         };

            double[] b ={
                            0,
                            0,
                            0,
                        };

            double[] tj = { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020 };

            double[] x0 = { 10.0054919341489, 3.03265795024749, 6.83122010827837, 1.98478460320379, 5.09293357450987, 4.05721328676762, 0.991215230484718 };

            double[,] I = { { 1, 0, 0, 0, 0, 0, 0 },
                            { 0, 1, 0, 0, 0, 0, 0 },
                            { 0, 0, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, 0, 0 },
                            { 0, 0, 0, 0, 0, 1, 0 },
                            { 0, 0, 0, 0, 0, 0, 1 },
            };

            double[,] W = { { 1/(tj[0]*tj[0]), 0, 0, 0, 0, 0, 0 },
                            { 0, 1/(tj[1]*tj[1]), 0, 0, 0, 0, 0 },
                            { 0, 0, 1/(tj[2]*tj[2]), 0, 0, 0, 0 },
                            { 0, 0, 0, 1/(tj[3]*tj[3]), 0, 0, 0 },
                            { 0, 0, 0, 0, 1/(tj[4]*tj[4]), 0, 0 },
                            { 0, 0, 0, 0, 0, 1/(tj[5]*tj[5]), 0 },
                            { 0, 0, 0, 0, 0, 0, 1/(tj[6]*tj[6]) },
            };
            int numberOfEqualities = 3;
            BalanceService balanceService = new();
            double[] res = balanceService.Calculate2(A, b, numberOfEqualities, x0, I, W);
            for (int i = 0; i < res.Length; i++)
                res[i] = Math.Round(res[i], 2);

            double[] Expected = new double[] { 10.06, 3.01, 7.04, 1.98, 5.06, 4.07, 0.99 };

            CollectionAssert.AreEqual(Expected, res);
        }

        [TestMethod]
        public void TestTwo()
        {
            double[,] A ={
                            { 1, -1, -1, 0, 0, 0, 0, -1 },
                            { 0, 0, 1, -1, -1, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, -1, -1, 0 },
                         };

            double[] b ={
                            0,
                            0,
                            0,
                        };

            double[] tj = { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020, 0.667 };

            double[] x0 = { 10.0054919341489, 3.03265795024749, 6.83122010827837, 1.98478460320379, 5.09293357450987, 4.05721328676762, 0.991215230484718, 6.66666 };

            double[,] I = { { 1, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 1, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 1, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 1, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 1 },
            };

            double[,] W = { { 1/(tj[0]*tj[0]), 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 1/(tj[1]*tj[1]), 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 1/(tj[2]*tj[2]), 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 1/(tj[3]*tj[3]), 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 1/(tj[4]*tj[4]), 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 1/(tj[5]*tj[5]), 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1/(tj[6]*tj[6]), 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 1/(tj[7]*tj[7]) },
            };
            int numberOfEqualities = 3;
            Calculation calc = new Calculation();
            double[] res = calc.balance(A, b, numberOfEqualities, x0, I, W);
            for (int i = 0; i < res.Length; i++)
                res[i] = Math.Round(res[i], 2);
            double[] Expected = new double[] { 10.54, 2.84, 6.97, 1.96, 5.01, 4.02, 0.99, 0.73 };

            CollectionAssert.AreEqual(Expected, res);
        }

        [TestMethod]
        public void TestThree()
        {
            double[,] A ={ {1, -1, -1, 0, 0, 0, 0, -1 },
                            { 0, 0, 1, -1, -1, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, -1, -1, 0 },
                            { 1, -10, 0, 0, 0, 0, 0, 0 },
                         };

            double[] b ={
                            0,
                            0,
                            0,
                            0,
                        };

            double[] tj = { 0.200, 0.121, 0.683, 0.040, 0.102, 0.081, 0.020, 0.667 };

            double[] x0 = { 10.0054919341489, 3.03265795024749, 6.83122010827837, 1.98478460320379, 5.09293357450987, 4.05721328676762, 0.991215230484718, 6.66666 };

            double[,] I = { { 1, 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 1, 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 1, 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 1, 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 1, 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 1, 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1, 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 1 },
            };

            double[,] W = { { 1/(tj[0]*tj[0]), 0, 0, 0, 0, 0, 0, 0 },
                            { 0, 1/(tj[1]*tj[1]), 0, 0, 0, 0, 0, 0 },
                            { 0, 0, 1/(tj[2]*tj[2]), 0, 0, 0, 0, 0 },
                            { 0, 0, 0, 1/(tj[3]*tj[3]), 0, 0, 0, 0 },
                            { 0, 0, 0, 0, 1/(tj[4]*tj[4]), 0, 0, 0 },
                            { 0, 0, 0, 0, 0, 1/(tj[5]*tj[5]), 0, 0 },
                            { 0, 0, 0, 0, 0, 0, 1/(tj[6]*tj[6]), 0 },
                            { 0, 0, 0, 0, 0, 0, 0, 1/(tj[7]*tj[7]) },
            };
            int numberOfEqualities = 4;
            Calculation calc = new Calculation();
            double[] res = calc.balance(A, b, numberOfEqualities, x0, I, W);

            double result = 0;

            for (int i = 0; i < 4; i++)
            {
                double resultj = 0;
                for (int j = 0; j < res.Length; j++)
                    resultj += A[i, j] * res[j];
                result += resultj * resultj;
            }
            Assert.AreEqual(0, Math.Sqrt(result), 0.001);//среднее квадратичное отклонение
            Assert.AreEqual(10.0, res[0] / res[1], 0.000001);

        }
    }
}
