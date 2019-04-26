using System.Collections.Generic;
using System.Threading.Tasks;
using KetoPal.Api.Controllers;
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
            configuration.Setup(x => x.GetSection("ConnectionStrings")["FoodDb"])
                .Returns("Server=10.0.75.1;Database=Foods;User Id=SA;Password=#newPass1");

            var productsController = new ProductsController(configuration.Object);

            ActionResult<List<Product>> response = await productsController.Get(0);
        }
    }
}
