using System.Collections.Generic;
using System.Threading.Tasks;
using KetoPal.Api.Controllers;
using KetoPal.Core.Models;
using KetoPal.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KetoPal.SlowTests
{
    [TestClass]
    public class ProductControllerIntegrationTests
    {
        [TestMethod]
        public async Task ReturnsSomething()
        {
            var configuration = new Mock<IConfiguration>();
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString), new InMemoryUsersProvider());

            ActionResult<List<Product>> response = await productsController.Get(0);

            var result = response.Result as ObjectResult;
            var products = result?.Value as List<Product>;
            Assert.IsNotNull(products);
        }

        [TestMethod]
        public async Task DataHasCarbContent()
        {
            var configuration = new Mock<IConfiguration>();
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString), new InMemoryUsersProvider());

            ActionResult<List<Product>> response = await productsController.Get(0);

            var result = response.Result as ObjectResult;
            var products = result?.Value as List<Product>;
            Assert.IsNotNull(products);
            Assert.IsTrue(products.TrueForAll(x => x.Carbs >= 0.0), "some products have carbs");
        }

        [TestMethod]
        public async Task AllHaveManufaturer()
        {
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString), new InMemoryUsersProvider());

            ActionResult<List<Product>> response = await productsController.Get(0);

            var result = response.Result as ObjectResult;
            var products = result?.Value as List<Product>;
            Assert.IsNotNull(products);
            Assert.IsTrue(products.TrueForAll(x => x.Manufacturer != null), "Some products don't have manufacturers");
        }

        [TestMethod]
        public async Task WhenIProviderUserId_ItReturnsSomething()
        {
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString), new InMemoryUsersProvider());

            ActionResult<List<Product>> response = await productsController.Get(1);

            var result = response.Result as ObjectResult;
            var products = result?.Value as List<Product>;
            Assert.IsNotNull(products);
            
        }

        [TestMethod]
        public async Task WhenIProvideUserId_ItFiltersTheResults()
        {
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString), new InMemoryUsersProvider());

            ActionResult<List<Product>> response0 = await productsController.Get(0);
            ActionResult<List<Product>> response1 = await productsController.Get(1);

            var result0 = response0.Result as ObjectResult;
            var products0 = result0?.Value as List<Product>;
            var result1 = response1.Result as ObjectResult;
            var products1 = result1?.Value as List<Product>;
            Assert.IsTrue(products0.Count > products1.Count);
        }
    }
}
