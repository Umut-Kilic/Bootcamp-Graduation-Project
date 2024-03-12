using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;

namespace BootcampApp.Repository.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BootcampAppDbContext context) : base(context)
        {

        }
    }
}
