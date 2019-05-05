using System.Threading.Tasks;
using KetoPal.Core.Models;

namespace KetoPal.Core
{
    public interface IUsersProvider
    {
        Task<User> FindUserById(int userId);
    }
}