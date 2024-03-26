using BootcampApp.Core.Models;
using NLayer.Core.Services;

namespace BootcampApp.Core.Services
{
    public interface IPostService : IService<Post>
    {
        public Task EditPostAsync(Post post, int[] categoryIds);
    }
}
