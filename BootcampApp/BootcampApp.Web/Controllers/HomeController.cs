using AutoMapper;
using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Core.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BootcampApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(IPostService postService, ICommentService commentService, IUserService userService, UserManager<User> userManager, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllAsync();
            //var postViewModel = _mapper.Map<PostViewModel>(products);
            var sliderImagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", "sliderimages");
            var imageNames = Directory.GetFiles(sliderImagePath)
                                       .Select(Path.GetFileName)
                                       .ToList();
            var sliderViewModel = new SliderViewModel
            {
                Images = imageNames
            };
            return View(new PostViewModel
            {
                Posts = posts.ToList(),
                SliderViewModel=sliderViewModel
            });
        }

      
        [HttpPost]
        public async Task<IActionResult> SignUp()
        {
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            return View(errorViewModel);
        }
    }
}
