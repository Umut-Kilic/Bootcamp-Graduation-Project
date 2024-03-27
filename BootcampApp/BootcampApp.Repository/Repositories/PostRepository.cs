using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BootcampApp.Repository.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {

        public PostRepository(BootcampAppDbContext context) : base(context)
        {

        }

        public async Task EditPostAsync(Post post)
        {
            var entity = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == post.PostId);

            entity.Title = post.Title;
            entity.Content = post.Content;
            entity.IsActive = post.IsActive;

            await _context.SaveChangesAsync();
        }

        public async Task EditPostAsync(Post post, int[] categoryIds)
        {
            var entity = await _context.Posts.Include(i => i.Categories).FirstOrDefaultAsync(i => i.PostId == post.PostId);
            entity.Title = post.Title;
            entity.Content = post.Content;
            entity.IsActive = post.IsActive;

            entity.Categories = _context.Categories.Where(c => categoryIds.Contains(c.CategoryId)).ToList();

            await _context.SaveChangesAsync();

        }


    }
}
