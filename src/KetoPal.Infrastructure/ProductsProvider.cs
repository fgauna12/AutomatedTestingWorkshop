using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using KetoPal.Core;

namespace KetoPal.Infrastructure
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly string _connectionString;

        public ProductsProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Product>> GetFoodProductsByCarbs()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // oh yea boil the ocean
                var products = await connection.QueryAsync<Product>("usp_Get_FoodProductsByCarbs", commandType: CommandType.StoredProcedure);

                return products.ToList();
            }
        }

        public async Task<List<Product>> GetFoodProductsByCarbsForUser(User user)
        {
            var products = await GetFoodProductsByCarbs();

            double consumption = user.CarbConsumption.Where(x => x.ConsumedOn.Date == DateTimeOffset.Now.Date)
                .Sum(x => x.Amount);

            double max = user.Preference.MaxCarbsPerDayInGrams - consumption;

            List<Product> productsThatCanBeConsumed = products.Where(x => x.Carbs <= max).ToList();

            return productsThatCanBeConsumed;
        }
    }
}
