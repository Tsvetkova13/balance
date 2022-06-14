using System;
using Xunit;
using MathNet.Numerics.LinearAlgebra;
using System.Text.Json;
using Newtonsoft.Json;
using BalaceController.Services;
using BalaceController.Models;
using BalaceController.Controllers;

namespace BalanceControllerTests
{
    public class BalanceTest
    {
        [Fact]
        public void Test1()
        {
            BalanceService calc = new BalanceService();
            string bi = @"{
         ""arrowInput"": [
            {
                        ""id"": 1,
              ""name"": ""X1"",
              ""sourceId"": null,
              ""destinationId"": 1,
              ""tolerance"": 0.2,
              ""measured"": 10.005,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 2,
              ""name"": ""X2"",
              ""sourceId"": 1,
              ""destinationId"": null,
              ""tolerance"": 0.121,
              ""measured"": 3.033,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 3,
              ""name"": ""X3"",
              ""sourceId"": 1,
              ""destinationId"": 2,
              ""tolerance"": 0.683,
              ""measured"": 6.831,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 4,
              ""name"": ""X4"",
              ""sourceId"": 2,
              ""destinationId"": null,
              ""tolerance"": 0.04,
              ""measured"": 1.985,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 5,
              ""name"": ""X5"",
              ""sourceId"": 2,
              ""destinationId"": 3,
              ""tolerance"": 0.102,
              ""measured"": 5.093,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 6,
              ""name"": ""X6"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.081,
              ""measured"": 4.057,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 7,
              ""name"": ""X7"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.02,
              ""measured"": 0.991,
              ""min"":0,
              ""max"":100
            }
         ]
        }";


            BalanceInput bali = JsonConvert.DeserializeObject<BalanceInput>(bi);
            BalanceOutput res = calc.balance(bali);
            double[] Rec = new double[res.arrowOutput.Count];
            for (int i = 0; i < res.arrowOutput.Count; i++)
                Rec[i] = Math.Round(res.arrowOutput[i].ReconcliedValue, 3);

            double[] Expected = new double[] { 10.056, 3.014, 7.041, 1.982, 5.059, 4.067, 0.992 };

            Assert.Equal(Expected, Rec);
        }

        [Fact]
        public void Test2()
        {
            BalanceService calc = new BalanceService();
            string bi = @"{
         ""arrowInput"": [
            {
                        ""id"": 1,
              ""name"": ""X1"",
              ""sourceId"": null,
              ""destinationId"": 1,
              ""tolerance"": 0.2,
              ""measured"": 10.0054919341489,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 2,
              ""name"": ""X2"",
              ""sourceId"": 1,
              ""destinationId"": null,
              ""tolerance"": 0.121,
              ""measured"": 3.03265795024749,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 3,
              ""name"": ""X3"",
              ""sourceId"": 1,
              ""destinationId"": 2,
              ""tolerance"": 0.683,
              ""measured"": 6.83122010827837,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 4,
              ""name"": ""X4"",
              ""sourceId"": 2,
              ""destinationId"": null,
              ""tolerance"": 0.04,
              ""measured"": 1.98478460320379,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 5,
              ""name"": ""X5"",
              ""sourceId"": 2,
              ""destinationId"": 3,
              ""tolerance"": 0.102,
              ""measured"": 5.09293357450987,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 6,
              ""name"": ""X6"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.081,
              ""measured"": 4.05721328676762,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 7,
              ""name"": ""X7"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.02,
              ""measured"": 0.991215230484718,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 8,
              ""name"": ""X8"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.667,
              ""measured"": 6.66666,
              ""min"":0,
              ""max"":100
            }
         ]
        }";


            BalanceInput bali = JsonConvert.DeserializeObject<BalanceInput>(bi);
            BalanceOutput res = calc.balance(bali);
            double[] Rec = new double[res.arrowOutput.Count];
            for (int i = 0; i < res.arrowOutput.Count; i++)
                Rec[i] = Math.Round(res.arrowOutput[i].ReconcliedValue, 2);

            double[] Expected = new double[] { 10.15, 2.98, 7.17, 1.98, 5.2, 3.96, 0.99, 0.25 };

            Assert.Equal(Expected, Rec);
        }

        [Fact]
        public void BalanseServiceStatus()
        {
            BalanceService calc = new BalanceService();
            string bi = @"{
         ""arrowInput"": [
            {
                        ""id"": 1,
              ""name"": ""X1"",
              ""sourceId"": null,
              ""destinationId"": 1,
              ""tolerance"": 0.2,
              ""measured"": 10.0054919341489,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 2,
              ""name"": ""X2"",
              ""sourceId"": 1,
              ""destinationId"": null,
              ""tolerance"": 0.121,
              ""measured"": 3.03265795024749,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 3,
              ""name"": ""X3"",
              ""sourceId"": 1,
              ""destinationId"": 2,
              ""tolerance"": 0.683,
              ""measured"": 6.83122010827837,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 4,
              ""name"": ""X4"",
              ""sourceId"": 2,
              ""destinationId"": null,
              ""tolerance"": 0.04,
              ""measured"": 1.98478460320379,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 5,
              ""name"": ""X5"",
              ""sourceId"": 2,
              ""destinationId"": 3,
              ""tolerance"": 0.102,
              ""measured"": 5.09293357450987,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 6,
              ""name"": ""X6"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.081,
              ""measured"": 4.05721328676762,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 7,
              ""name"": ""X7"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.02,
              ""measured"": 0.991215230484718,
              ""min"":0,
              ""max"":100
            },
            {
                        ""id"": 8,
              ""name"": ""X8"",
              ""sourceId"": 3,
              ""destinationId"": null,
              ""tolerance"": 0.667,
              ""measured"": 6.66666,
              ""min"":0,
              ""max"":100
            }
         ]
        }";


            BalanceInput bali = JsonConvert.DeserializeObject<BalanceInput>(bi);
            BalanceOutput res = calc.balance(bali);
            double[] Rec = new double[res.arrowOutput.Count];
            for (int i = 0; i < res.arrowOutput.Count; i++)
                Rec[i] = Math.Round(res.arrowOutput[i].ReconcliedValue, 2);

            double[] Expected = new double[] { 10.15, 2.98, 7.17, 1.98, 5.2, 3.96, 0.99, 0.25 };

            Assert.Equal("Success", res.Status);
        }
    }
}
