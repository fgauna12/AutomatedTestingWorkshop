using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KetoPal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET api/products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Product>> Get()
        {
            return Ok(
                new List<Product>()
                {
                    new Product()
                    {
                        Name = "Oreos"
                    }
                }
            );
        }
    }

    public class Product
    {
        public string Name { get; set; }
    }
}
