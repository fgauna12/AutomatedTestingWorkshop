using System.Collections.Generic;
using System.Threading.Tasks;
using KetoPal.Api.Controllers;
using KetoPal.Core;
using KetoPal.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace KetoPal.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task x()
        {
            var configuration = new Mock<IConfiguration>();
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString));

            ActionResult<List<Product>> response = await productsController.Get(0);
        }

        //[TestMethod]
        //public async Task x1()
        //{
        //    //do they all have carb content?

        //    var configuration = new Mock<IConfiguration>();
        //    configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
        //        .Returns("Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1");

        //    var productsController = new ProductsController(configuration.Object);

        //    ActionResult<List<Product>> response = await productsController.Get(0);

        //    var result = response.Result as ObjectResult;
        //    var products = result?.Value as List<Product>;
        //    Assert.IsNotNull(products);
        //    Assert.IsTrue(products.TrueForAll(x => x.Carbs > 0.0), "some products have carbs");
        //}

        [TestMethod]
        public async Task x2()
        {
            //do they all have manufacturer?
            
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString));

            ActionResult<List<Product>> response = await productsController.Get(0);

            var result = response.Result as ObjectResult;
            var products = result?.Value as List<Product>;
            Assert.IsNotNull(products);
            Assert.IsTrue(products.TrueForAll(x => x.Manufacturer != null), "Some products don't have manufacturers");
        }

        [TestMethod]
        public async Task x3()
        {
            //what happens if I provide a user id?
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString));

            ActionResult<List<Product>> response = await productsController.Get(1);

            var result = response.Result as ObjectResult;
            var products = result?.Value as List<Product>;
            Assert.IsNotNull(products);
            
        }

        [TestMethod]
        public async Task x4()
        {
            //there seems to be some filtering for some users
            var connectionString = "Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1";
            var configuration = new Mock<IConfiguration>();
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns(connectionString);

            var productsController = new ProductsController(new ProductsProvider(connectionString));

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
