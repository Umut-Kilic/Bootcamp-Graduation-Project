using BootcampApp.Core.Models;

namespace BootcampApp.Core.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        public Task EditPostAsync(Post post);
        public Task EditPostAsync(Post post, int[] categoryIds);
    }
}
