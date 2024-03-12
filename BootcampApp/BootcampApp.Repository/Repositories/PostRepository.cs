using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;

namespace BootcampApp.Repository.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BootcampAppDbContext context) : base(context)
        {

        }
    }
}
