using AutoMapper;
using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Security.Claims;

namespace BootcampApp.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ICategoryService _categoryService;
        private readonly IFileProvider _fileProvider;
        private IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public PostsController(IPostService postService, ICommentService commentService, IUserService userService, UserManager<User> userManager, IMapper mapper, ICategoryService categoryService, IFileProvider fileProvider)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _categoryService = categoryService;
            _fileProvider = fileProvider;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {

            IQueryable<Post> postsQuery = _postService.GetAll().Include(p => p.Categories);

            if (categoryId != null)
            {
                postsQuery = postsQuery.Where(p => p.Categories.Any(c => c.CategoryId == categoryId)).Include(p => p.Categories);
            }

            List<Post> posts = await postsQuery.ToListAsync();

            return View(new PostsViewModel
            {
                Posts = posts
            });
        }

        public async Task<IActionResult> Details(int postId)
        {
            var post = await _postService
                            .GetAll()
                            .Include(x => x.User)
                            .Include(x => x.Categories)
                            .Include(x => x.Comments)
                            .ThenInclude(x => x.User)
                            .FirstOrDefaultAsync(p => p.PostId == postId);

            return View(post);
        }


        [HttpPost]
        public async Task<JsonResult> AddComment(int PostId, string Text)
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var username = User.FindFirstValue(ClaimTypes.Name);
            var user = await _userManager.FindByIdAsync(userId);
            var avatar = user!.Picture;
            var entity = new Comment
            {
                PostId = PostId,
                Text = Text,
                PublishedDate = DateTime.Now,
                UserId = userId,
                LikeCount = 0

            };
            await _commentService.AddAsync(entity);
            return Json(new
            {
                username,
                Text,
                entity.PublishedDate,
                avatar,
                likeCount = entity.LikeCount
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Like(int postId, int Count)
        {
            var post = await _postService.GetByIdAsync(postId);
            if (post == null)
            {
                return Json(new { success = false });
            }
            post.LikeCount = Count;
            await _postService.UpdateAsync(post);

            return Json(new { success = true, Like = post.LikeCount });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Unlike(int postId, int Count)
        {
            var post = await _postService.GetByIdAsync(postId);
            if (post == null)
            {
                return Json(new { success = false });
            }
            post.LikeCount = Count;
            await _postService.UpdateAsync(post);

            return Json(new { success = true, Like = post.LikeCount });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new PostCreateViewModel
            {
                Categories = await _categoryService.GetAll().ToListAsync(),
            });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostCreateViewModel request)
        {

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currentUser = await _userManager.FindByIdAsync(userId);

                if (request.Picture != null && request.Picture.Length > 0)
                {
                    var wwwrootFolder = _fileProvider.GetDirectoryContents("wwwroot");

                    string randomFileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(request.Picture.FileName)}";

                    var newPicturePath = Path.Combine(wwwrootFolder!.First(x => x.Name == "postspictures").PhysicalPath!, randomFileName);

                    using var stream = new FileStream(newPicturePath, FileMode.Create);

                    await request.Picture.CopyToAsync(stream);


                    var newPost = new Post
                    {
                        Title = request.Title,
                        Content = request.Content,
                        UserId = userId,
                        PublishedDate = DateTime.Now,
                        Image = randomFileName,
                        IsActive = true
                    };

                    foreach (var categoryId in request.SelectedCategories)
                    {
                        var category = await _categoryService.GetByIdAsync(categoryId);
                        newPost.Categories.Add(category);
                    }

                    await _postService.AddAsync(newPost);
                    return RedirectToAction("MyPosts", "Member");
                }
                await _postService.AddAsync(new Post
                {
                    Title = request.Title,
                    Content = request.Content,
                    UserId = userId,
                    PublishedDate = DateTime.Now,
                    IsActive = true
                });

                return RedirectToAction("MyPosts", "Member");


            }
            return View(request);
        }


        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = _postService.GetAll().Include(i => i.Categories).FirstOrDefault(i => i.PostId == id);
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

        [Authorize]
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

                if (User.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "admin"))
                {
                    entityToUpdate.IsActive = model.IsActive;
                }

                await _postService.EditPostAsync(entityToUpdate, categoryIds);
                return RedirectToAction("MyPosts", "Member");
            }
            ViewBag.Categories = await _categoryService.GetAll().ToListAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? postId)
        {
            if (postId == null)
            {
                return Json(new { Success = false, Error = new string("Böyle bir idye sahip post bulunamadı") });
            }
            var post = await _postService.GetByIdAsync(postId);

            if (post == null)
            {
                return Json(new { Success = false, Error = new string("Böyle bir post bulunamadı") });
            }
            await _postService.RemoveAsync(post);

            return Json(new { Success = true });
        }


    }
}
