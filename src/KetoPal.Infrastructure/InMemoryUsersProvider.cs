using System.Linq;
using System.Threading.Tasks;
using KetoPal.Core;
using KetoPal.Core.Models;

namespace KetoPal.Infrastructure
{
    public class InMemoryUsersProvider : IUsersProvider
    {
        public Task<User> FindUserById(int userId)
        {
            var user = InMemoryUsers.GetUsers().FirstOrDefault(x => x.Id == userId);
            return Task.FromResult(user);
        }
    }
}