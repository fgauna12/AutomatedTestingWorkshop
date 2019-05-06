using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using KetoPal.Api.Controllers;
using KetoPal.Core;
using KetoPal.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KetoPal.Tests
{
    [TestClass]
    public class ProductsControllerUnitTests
    {
        private Mock<IProductsProvider> _productsProviderMock;
        private ProductsController _classUnderTest;
        private Mock<IUsersProvider> _usersProviderMock;

        [TestInitialize]
        public void SetUp()
        {
            _productsProviderMock = new Mock<IProductsProvider>();
            _usersProviderMock = new Mock<IUsersProvider>();
            _classUnderTest = new ProductsController(_productsProviderMock.Object, _usersProviderMock.Object);
        }

        [TestMethod]
        public async Task GetProducts_NoUserId_RequestsAllProducts()
        {
            //Arrange

            //Act
            ActionResult<List<Product>> response = await _classUnderTest.Get(0);

            //Assert
            _productsProviderMock.Verify(x => x.GetFoodProductsByCarbs());
        }

        [TestMethod]
        public async Task GetProducts_NoUserId_ReturnsAllProductsFromProvider()
        {
            //Arrange
            var products = new List<Product>()
            {
                new Product() {Carbs = 1.1, Manufacturer = "Jimmy Goods", Name = "Jimmy Thing"}
            };
            _productsProviderMock.Setup(x => x.GetFoodProductsByCarbs()).ReturnsAsync(products);

            //Act
            ActionResult<List<Product>> response = await _classUnderTest.Get(0);

            //Assert
            var result = response.Result as ObjectResult;
            var productResults = result?.Value as List<Product>;
            productResults.Should().NotBeNull();
            productResults.Count.Should().Be(products.Count);
            productResults.TrueForAll(x => products.Contains(x)).Should().BeTrue();
        }

        [TestMethod]
        public async Task GetProducts_HasUserId_TriesToFindUser()
        {
            //Arrange
            var userId = 5;

            //Act
            ActionResult<List<Product>> response = await _classUnderTest.Get(userId);

            //Assert
            _usersProviderMock.Verify(x => x.FindUserById(userId));
        }

        [TestMethod]
        public async Task GetProducts_HasUserId_TriesToGetProductsForUser()
        {
            //Arrange
            var userId = 5;

            _usersProviderMock.Setup(x => x.FindUserById(userId)).ReturnsAsync(new User()
            {
                Id = userId,
                UserName = "test user"
            });

            //Act
            ActionResult<List<Product>> response = await _classUnderTest.Get(userId);

            //Assert
            _productsProviderMock.Verify(x => x.GetFoodProductsByCarbsForUser(It.Is<User>(user => 
                user != null && user.Id == userId)));
        }
    }
}