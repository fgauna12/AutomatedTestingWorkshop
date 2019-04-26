using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using KetoPal.Core;
using KetoPal.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace KetoPal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductsProvider _productsProvider;

        public ProductsController(IConfiguration configuration, IProductsProvider productsProvider)
        {
            _configuration = configuration;
            _productsProvider = productsProvider;
        }

        // GET api/products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Product>>> Get([FromQuery] int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("FoodDb")))
            {
                await connection.OpenAsync();

                // oh yea boil the ocean
                //var products = await connection.QueryAsync<Product>("usp_Get_FoodProductsByCarbs",
                //    commandType: CommandType.StoredProcedure);

                var products = await _productsProvider.GetFoodProductsByCarbs();

                if (userId > 0)
                {
                    //var user = InMemoryUsers.GetUsers().FirstOrDefault(x => x.Id == userId);

                    //double consumption = user.CarbConsumption.Where(x => x.ConsumedOn.Date == DateTimeOffset.Now.Date)
                    //    .Sum(x => x.Amount);

                    //double max = user.Preference.MaxCarbsPerDayInGrams - consumption;

                    //List<Product> productsThatCanBeConsumed = products.Where(x => x.Carbs <= max).ToList();

                    //return Ok(productsThatCanBeConsumed);

                    var user = InMemoryUsers.GetUsers().FirstOrDefault(x => x.Id == userId);
                    products = await _productsProvider.GetFoodProductsByCarbsForUser(user);
                }

                return Ok(products.ToList());
            }
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
