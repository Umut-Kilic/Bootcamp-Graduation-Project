using AutoMapper;
using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BootcampApp.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, ICommentService commentService, IUserService userService, UserManager<User> userManager, IMapper mapper)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _postService.GetAllAsync();
            var postViewModel = _mapper.Map<PostViewModel>(products);
            return View(postViewModel);
        }
    }
}
