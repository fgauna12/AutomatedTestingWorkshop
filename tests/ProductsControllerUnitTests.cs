using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using KetoPal.Api.Controllers;
using KetoPal.Core;
using KetoPal.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KetoPal.Tests
{
    [TestClass]
    public class ProductsControllerUnitTests
    {
        private Mock<IProductsProvider> _productsProviderMock;
        private ProductsController _classUnderTest;

        [TestInitialize]
        public void SetUp()
        {
            _productsProviderMock = new Mock<IProductsProvider>();
            _classUnderTest = new ProductsController(_productsProviderMock.Object);
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
    }
}