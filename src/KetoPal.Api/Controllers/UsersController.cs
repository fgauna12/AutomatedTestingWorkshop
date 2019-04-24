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
    public class UsersController : ControllerBase
    {
        // GET api/users
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            List<User> users = InMemoryUsers.GetUsers();
            return Ok(users);
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Preference Preference { get; set; }
        public List<CarbConsumption> CarbConsumption { get; set; }
    }

    public class Preference
    {
        public double MaxCarbsPerDayInGrams { get; set; }
    }

    public class CarbConsumption
    {
        public DateTimeOffset ConsumedOn { get; set; }
        public double Amount { get; set; }
    }
}