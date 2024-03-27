using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Core.ViewModels;
using BootcampApp.Service.Services;
using BootcampApp.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PostsViewModel = BootcampApp.Web.Areas.Admin.Models.PostsViewModel;

namespace BootcampApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;

        public PostsController(IPostService postService, ICategoryService categoryService)
        {
            _postService = postService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Lists()
        {
            var posts = await _postService.GetAll().ToListAsync();
            return View(new PostsViewModel
            {
                Posts = posts
            });
        }

        public async Task<IActionResult> Edit(int? postId)
        {
            if (postId == null)
            {
                return NotFound();
            }
            var post = _postService.GetAll().Include(i => i.Categories).FirstOrDefault(i => i.PostId == postId);
            if (post == null)
            {
                return NotFound();
            }
            ViewBag.Categories = await _categoryService.GetAll().ToListAsync();

            return View(new PostCreateViewModel
            {
                PostId = post.PostId,
                Title = post.Title,
                Content = post.Content,
                IsActive = post.IsActive,
                Categories = post.Categories
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PostCreateViewModel model, int[] categoryIds)
        {
            if (ModelState.IsValid)
            {
                var entityToUpdate = new Post
                {
                    PostId = model.PostId,
                    Title = model.Title,
                    Content = model.Content,
                };

                    entityToUpdate.IsActive = model.IsActive;

                await _postService.EditPostAsync(entityToUpdate, categoryIds);
                return RedirectToAction("Lists", "Posts");
            }
            ViewBag.Categories = await _categoryService.GetAll().ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? postId)
        {
            if (postId == null)
            {
                return Json(new { Success=false, Error=new string("Böyle bir idye sahip post bulunamadı")});
            }
            var post=await _postService.GetByIdAsync(postId);

            if (post == null)
            {
                return Json(new { Success = false, Error = new string("Böyle bir post bulunamadı") });
            }
            await _postService.RemoveAsync(post);

            return Json(new { Success = true });
        }
    }
}
