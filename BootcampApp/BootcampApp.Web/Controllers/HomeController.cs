using AutoMapper;
using Azure.Core;
using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Core.ViewModels;
using BootcampApp.Web.Extenisons;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace BootcampApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        public HomeController(IPostService postService, ICommentService commentService, IUserService userService, UserManager<User> userManager, IMapper mapper, IWebHostEnvironment hostEnvironment, SignInManager<User> signInManager)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _signInManager = signInManager;
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
                SliderViewModel = sliderViewModel
            });
        }


        [HttpPost]
        public async Task<IActionResult> SignUp(PostViewModel request)
        {
            var posts = await _postService.GetAllAsync();
            var sliderImagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", "sliderimages");
            var imageNames = Directory.GetFiles(sliderImagePath)
                                       .Select(Path.GetFileName)
                                       .ToList();
            var sliderViewModel = new SliderViewModel
            {
                Images = imageNames
            };

            var signUpViewModelState = ModelState.GetFieldValidationState("SignUpViewModel");
            if (signUpViewModelState == ModelValidationState.Invalid)
            {
                return RedirectToAction(nameof(HomeController.Index), new PostViewModel
                {
                    Posts = posts.ToList(),
                    SliderViewModel = sliderViewModel
                });
            }
           // var userValidationResult = await _userValidator.ValidateAsync(_userManager, newUser);



            var identityResult = await _userManager.CreateAsync(new() { UserName = request.SignUpViewModel.UserName, Email = request.SignUpViewModel.Email }, request.SignUpViewModel.PasswordConfirm);


            if (!identityResult.Succeeded)
            {
                ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
                return View(nameof(HomeController.Index), new PostViewModel
                {
                    Posts = posts.ToList(),
                    SliderViewModel = sliderViewModel
                });
            }        

            var user = await _userManager.FindByNameAsync(request.SignUpViewModel.UserName);

            
            return RedirectToAction(nameof(HomeController.Index), new PostViewModel
            {
                Posts = posts.ToList(),
                SliderViewModel = sliderViewModel
            });
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(PostViewModel reguest, string? returnUrl = null)
        {
            var posts = await _postService.GetAllAsync();
            var sliderImagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", "sliderimages");
            var imageNames = Directory.GetFiles(sliderImagePath)
                                       .Select(Path.GetFileName)
                                       .ToList();
            var sliderViewModel = new SliderViewModel
            {
                Images = imageNames
            };

            var signInViewModelState = ModelState.GetFieldValidationState("SignInViewModel");
            if (signInViewModelState == ModelValidationState.Invalid)
            {
                return RedirectToAction(nameof(HomeController.Index), new PostViewModel
                {
                    Posts = posts.ToList(),
                    SliderViewModel = sliderViewModel
                });
            }

            returnUrl ??= Url.Action("Index", "Home");

            var hasUser = await _userManager.FindByEmailAsync(reguest.SignInViewModel.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya þifre yanlýþ");
                return RedirectToAction(nameof(HomeController.Index), new PostViewModel
                {
                    Posts = posts.ToList(),
                    SliderViewModel = sliderViewModel
                });
            }

            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, reguest.SignInViewModel.Password, reguest.SignInViewModel.RememberMe, true);


            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelErrorList(new List<string>() { "3 dakika boyunca giriþ yapamazsýnýz." });
                return RedirectToAction(nameof(HomeController.Index), new PostViewModel
                {
                    Posts = posts.ToList(),
                    SliderViewModel = sliderViewModel
                });
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelErrorList(new List<string>() { $"Email veya þifre yanlýþ", $"Baþarýsýz giriþ sayýsý = {await _userManager.GetAccessFailedCountAsync(hasUser)}" });
                return RedirectToAction(nameof(HomeController.Index), new PostViewModel
                {
                    Posts = posts.ToList(),
                    SliderViewModel = sliderViewModel
                });
            }

            return Redirect(returnUrl!);

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
