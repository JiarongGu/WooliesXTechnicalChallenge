using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WooliesXTechnicalChallenge.Models;
using WooliesXTechnicalChallenge.Services;
using WooliesXTechnicalChallenge.UnitTest.Models;
using Xunit;

namespace WooliesXTechnicalChallenge.UnitTest.Services
{
    public class TrolleyCalculatorServiceUnitTest
    {
        private readonly IEnumerable<TrolleyTest> _trolleyTests;

        public TrolleyCalculatorServiceUnitTest() {
            string trolleiesJsonPath = Path.Combine(
                Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location), @"Jsons\trolleies.json");
            var JsonText = File.ReadAllText(trolleiesJsonPath);

            _trolleyTests = JsonConvert.DeserializeObject<IEnumerable<TrolleyTest>>(JsonText);
        }

        [Fact]
        public void TrolleyTotal_No_Specials()
        {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var request = GetTrolleyCalculatorRequest("Test Product A", 100, 10, null);
            var total = trolleyCalculatorService.CalculateTrolley(request);

            Assert.Equal(1000, total);
        }

        [Fact]
        public void TrolleyTotal_Single_Special()
        {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var request = GetTrolleyCalculatorRequest("Test Product A", 100, 10,
                    new List<(int, decimal)>() {
                        (2, 160)
                    }
                );
            var total = trolleyCalculatorService.CalculateTrolley(request);

            Assert.Equal(800, total);
        }

        [Fact]
        public void TrolleyTotal_Multiple_Specials_Only_One_Taken()
        {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var request = GetTrolleyCalculatorRequest("Test Product A", 100, 10,
                    new List<(int, decimal)>() {
                        (2, 160),
                        (3, 210)
                    }
                );
            var total = trolleyCalculatorService.CalculateTrolley(request);

            Assert.Equal(730, total);
        }

        [Fact]
        public void TrolleyTotal_Multiple_Specials_All_Taken()
        {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var request = GetTrolleyCalculatorRequest("Test Product A", 100, 11,
                    new List<(int, decimal)>() {
                        (2, 160),
                        (3, 210)
                    }
                );
            var total = trolleyCalculatorService.CalculateTrolley(request);

            Assert.Equal(790, total);
        }

        [Fact]
        public void TrolleyTotal_No_Products()
        {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var request = new Trolley()
            {
                Specials = new List<Special>
                {
                    new Special(),
                    new Special(),
                    new Special(),
                    new Special()
                }
            };

            var total = trolleyCalculatorService.CalculateTrolley(request);
            Assert.Equal(0, total);
        }

        [Fact]
        public void TrolleyTotal_Complex_Success() {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var trolleyTest = GetTrolleyTest("Complex");

            var total = trolleyCalculatorService.CalculateTrolley(trolleyTest.Trolley);
            Assert.Equal(trolleyTest.Total, total);
        }

        [Fact]
        public void TrolleyTotal_Complex_Simple()
        {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var trolleyTest = GetTrolleyTest("Simple");

            var total = trolleyCalculatorService.CalculateTrolley(trolleyTest.Trolley);
            Assert.Equal(trolleyTest.Total, total);
        }

        [Fact]
        public void TrolleyTotal_Complex_Simple1()
        {
            var trolleyCalculatorService = new TrolleyCalculatorService();
            var trolleyTest = GetTrolleyTest("Simple1");

            var total = trolleyCalculatorService.CalculateTrolley(trolleyTest.Trolley);
            Assert.Equal(trolleyTest.Total, total);
        }

        private TrolleyTest GetTrolleyTest(string name)
        {
            return _trolleyTests.First(x => x.Name == name);
        }

        private Trolley GetTrolleyCalculatorRequest(
           string productName,
           decimal price,
           int quantity,
           IEnumerable<(int, decimal)> specials = null
           )
        {
            return new Trolley()
            {
                Products = new List<Product>
                {
                    new Product() { Name = productName, Price = price }
                },
                Specials = specials?.Select(x => new Special()
                {
                    Quantities = new List<Quantity>() {
                        new Quantity() { Name = productName, Value = x.Item1 }
                    },
                    Total = x.Item2
                }),
                Quantities = new List<Quantity> {
                    new Quantity() {
                        Name = productName,
                        Value = quantity
                    }
                }
            };
        }
    }
}
