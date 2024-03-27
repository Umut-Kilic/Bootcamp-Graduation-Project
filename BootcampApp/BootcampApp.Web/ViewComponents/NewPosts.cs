using BootcampApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootcampApp.ViewComponents
{
    public class NewPosts : ViewComponent
    {
        private IPostService _postService;

        public NewPosts(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _postService
                .GetAll()
                .OrderByDescending(p => p.PublishedDate)
                .Take(5)
                .ToListAsync());

        }
    }
}