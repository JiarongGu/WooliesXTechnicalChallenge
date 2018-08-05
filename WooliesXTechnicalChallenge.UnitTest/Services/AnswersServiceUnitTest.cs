using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WooliesXTechnicalChallenge.Enums;
using WooliesXTechnicalChallenge.Models;
using WooliesXTechnicalChallenge.Options;
using WooliesXTechnicalChallenge.Services;
using Xunit;
namespace WooliesXTechnicalChallenge.UnitTest.Services
{
    public class AnswersServiceUnitTest
    {
        private readonly Mock<IResourceService> _resourceServiceMock = new Mock<IResourceService>();
        private readonly Mock<IOptions<TesterSettings>> _testerSettingsMock = new Mock<IOptions<TesterSettings>>();

        public AnswersServiceUnitTest()
        {
            var products = new List<Product> {
                new Product("Test Product A", 99.99m, 0.0m),
                new Product("Test Product B", 101.99m, 0.0m),
                new Product("Test Product C", 10.99m, 0.0m),
                new Product("Test Product D", 5.0m, 0.0m),
                new Product("Test Product F", 999999999999.0m, 0.0m)
            };

            var shopperHistory = new ShopperHistory()
            {
                CustomerId = 1,
                Products = new List<Product>
                {
                    new Product("Test Product B", 101.99m, 0.0m),
                    new Product("Test Product C", 10.99m, 0.0m),
                    new Product("Test Product D", 5.0m, 0.0m)
                }
            };

            _resourceServiceMock.Setup(x => x.GetProducts()).Returns(products);
            _resourceServiceMock.Setup(x => x.GetShopperHisotry()).Returns(shopperHistory);

            _testerSettingsMock.SetupGet(x => x.Value).Returns(new TesterSettings() { Name = "tester" });
        }

        [Fact]
        public void GetProduct_By_Low_Success() {
            var answerService = GetAnswersService();
            var products = answerService.GetProducts(SortOptions.Low).ToArray();

            Assert.Equal(5.0m, products[0].Price);
            Assert.Equal(10.99m, products[1].Price);
            Assert.Equal(99.99m, products[2].Price);
            Assert.Equal(101.99m, products[3].Price);
            Assert.Equal(999999999999.0m, products[4].Price);
        }

        [Fact]
        public void GetProduct_By_High_Success()
        {
            var answerService = GetAnswersService();
            var products = answerService.GetProducts(SortOptions.High).ToArray();

            Assert.Equal(999999999999.0m, products[0].Price);
            Assert.Equal(101.99m, products[1].Price);
            Assert.Equal(99.99m, products[2].Price);
            Assert.Equal(10.99m, products[3].Price);
            Assert.Equal(5.0m, products[4].Price);
        }

        [Fact]
        public void GetProduct_By_Ascending_Success()
        {
            var answerService = GetAnswersService();
            var products = answerService.GetProducts(SortOptions.Ascending).ToArray();

            Assert.Equal("Test Product A", products[0].Name);
            Assert.Equal("Test Product B", products[1].Name);
            Assert.Equal("Test Product C", products[2].Name);
            Assert.Equal("Test Product D", products[3].Name);
            Assert.Equal("Test Product F", products[4].Name);
        }

        [Fact]
        public void GetProduct_By_Descending_Success()
        {
            var answerService = GetAnswersService();
            var products = answerService.GetProducts(SortOptions.Descending).ToArray();

            Assert.Equal("Test Product F", products[0].Name);
            Assert.Equal("Test Product D", products[1].Name);
            Assert.Equal("Test Product C", products[2].Name);
            Assert.Equal("Test Product B", products[3].Name);
            Assert.Equal("Test Product A", products[4].Name);
        }

        [Fact]
        public void GetProduct_By_Recommanded_Success()
        {
            var answerService = GetAnswersService();
            var products = answerService.GetProducts(SortOptions.Recommended).ToArray();

            Assert.Equal(3, products.Count());
            Assert.Equal("Test Product B", products[0].Name);
            Assert.Equal("Test Product C", products[1].Name);
            Assert.Equal("Test Product D", products[2].Name);
        }

        [Fact]
        public void GetTesterSettings_Success() {
            var answerService = GetAnswersService();
            var testerSetting = answerService.GetTesterSettings();

            Assert.Equal("tester", testerSetting.Name);
        }

        private IAnswersService GetAnswersService() {
            return new AnswersService(_resourceServiceMock.Object, _testerSettingsMock.Object);
        }
    }
}
