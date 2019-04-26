using System.Collections.Generic;
using System.Threading.Tasks;
using KetoPal.Core.Models;

namespace KetoPal.Core
{
    public interface IProductsProvider
    {
        Task<List<Product>> GetFoodProductsByCarbs();
        Task<List<Product>> GetFoodProductsByCarbsForUser(User user);
    }
}