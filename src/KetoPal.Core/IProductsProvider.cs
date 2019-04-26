using System.Collections.Generic;
using System.Threading.Tasks;

namespace KetoPal.Core
{
    public interface IProductsProvider
    {
        Task<List<Product>> GetFoodProductsByCarbs();
    }
}