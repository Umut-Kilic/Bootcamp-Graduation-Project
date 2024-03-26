using BootcampApp.Core.Services;
using BootcampApp.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootcampApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Lists()
        {
            var posts=await _postService.GetAll().ToListAsync();
            return View(new PostsViewModel
            {
                Posts=posts
            });
        }
    }
}
