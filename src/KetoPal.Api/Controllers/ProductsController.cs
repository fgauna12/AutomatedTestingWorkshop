using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
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

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // GET api/products
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Product>>> Get([FromQuery] int userId)
        {
            using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                await connection.OpenAsync();

                // oh yea boil the ocean
                var products = await connection.QueryAsync<Product>("usp_Get_FoodProductsByCarbs", commandType: CommandType.StoredProcedure);

                if (userId > 0)
                {
                    var user = InMemoryUsers.GetUsers().FirstOrDefault(x => x.Id == userId);

                    double consumption = user.CarbConsumption.Where(x => x.ConsumedOn.Date == DateTimeOffset.Now.Date).Sum(x => x.Amount);

                    double max = user.Preference.MaxCarbsPerDayInGrams - consumption;

                    List<Product> productsThatCanBeConsumed = products.Where(x => x.Carbs <= max).ToList();

                    return Ok(productsThatCanBeConsumed);
                }

                return Ok(products.ToList());
            }
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public double Carbs { get; set; }
    }
}
