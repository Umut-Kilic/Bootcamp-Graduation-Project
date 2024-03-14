using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;

namespace BootcampApp.Repository.Repositories
{

    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BootcampAppDbContext context) : base(context)
        {

        }
    }
}
