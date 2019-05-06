using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using KetoPal.Core;
using KetoPal.Core.Models;
using KetoPal.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KetoPal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsProvider _productsProvider;
        private readonly IUsersProvider _usersProvider;

        public ProductsController(IProductsProvider productsProvider, IUsersProvider usersProvider)
        {
            _productsProvider = productsProvider;
            _usersProvider = usersProvider;
        }

        // GET api/products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Product>>> Get([FromQuery] int userId)
        {
            List<Product> products;

            if (userId > 0)
            {
                User user = await _usersProvider.FindUserById(userId);
                products = await _productsProvider.GetFoodProductsByCarbsForUser(user);
            }
            else
            {
                products = await _productsProvider.GetFoodProductsByCarbs();
            }

            products = products ?? new List<Product>();

            return Ok(products.ToList());
        }

        // POST api/products/_actions/consume
        [Route("_actions/consume")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<ActionResult> Consume([FromBody]ConsumeProductCommand command)
        {
            if (command.UserId > 0)
            {
                var user = InMemoryUsers.GetUsers().FirstOrDefault(x => x.Id == command.UserId);
                user.CarbConsumption.Add(new CarbConsumption()
                {
                    Amount = command.CarbAmount,
                    ConsumedOn = DateTimeOffset.Now
                });

                return NotModified();
            }
            else
            {
                return NotFound();
            }
        }

        private ActionResult NotModified()
        {
            return new EmptyResult();
        }
    }

    public class ConsumeProductCommand
    {
        public int UserId { get; set; }
        public double CarbAmount { get; set; }
    }
}
