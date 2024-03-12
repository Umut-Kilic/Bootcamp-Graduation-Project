using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;

namespace BootcampApp.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BootcampAppDbContext context) : base(context)
        {
        }
    }
}
