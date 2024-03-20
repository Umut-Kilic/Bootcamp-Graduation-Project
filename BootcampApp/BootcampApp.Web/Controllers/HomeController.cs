using AutoMapper;
using BootcampApp.Core.Models;
using BootcampApp.Core.Services;
using BootcampApp.Core.ViewModels;
using BootcampApp.Web.Extenisons;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IEmailService _emailService;
        public HomeController(IPostService postService, ICommentService commentService, IUserService userService, UserManager<User> userManager, IMapper mapper, IWebHostEnvironment hostEnvironment, SignInManager<User> signInManager, IEmailService emailService)
        {
            _postService = postService;
            _commentService = commentService;
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _signInManager = signInManager;
            _emailService = emailService;
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

            return View(new IndexViewModel
            {
                Posts = posts.ToList(),
                SliderViewModel = sliderViewModel
            });
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!request.PermissionAllow)
            {
                ModelState.AddModelError(string.Empty, "Kullanýcý sözleþmesini kabul etmek zorundasýnýz");
                var signUpErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = signUpErrors, isCheck = false });
            }

            if (!ModelState.IsValid)
            {
                var signUpErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = signUpErrors, isCheck = true });
            }

            var identityResult = await _userManager.CreateAsync(new() { UserName = request.UserName, Email = request.Email }, request.PasswordConfirm);

            if (!identityResult.Succeeded)
            {
                var signUpErrors = identityResult.Errors.Select(x => x.Description).ToList();
                return Json(new { success = false, errors = signUpErrors, isCheck = true });
            }

            var user = await _userManager.FindByNameAsync(request.UserName);

            return Json(new { success = true, user, isCheck = true });
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel request)
        {
            if (!ModelState.IsValid)
            {
                var resetPasswordErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = resetPasswordErrors, isCheck = true });
            }
            var hasUser = await _userManager.FindByEmailAsync(request.Email);
            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Bu Email adresine sahip kullanýcý bulunamamýþtýr");
                var resetPasswordErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();

                return Json(new { success = false, errors = resetPasswordErrors });
            }


            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(hasUser);

            var passwordResetLink = Url.Action("ResetPassword", "Home", new { UserId = hasUser.Id, Token = passwordResetToken },
                HttpContext.Request.Scheme);

            await _emailService.SendResetPasswordEmailAsync(passwordResetLink, hasUser.Email);


            return Json(new { success = true });
        }


        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            TempData["userId"] = userId;
            TempData["token"] = token;      

    

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel request ,string userId, string token)
        {
           

            if (userId == null || token == null)
            {
                throw new Exception("Bir hata meydana geldi");
            }

            if (!ModelState.IsValid)
            {
                var resetPasswordErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = resetPasswordErrors });
            }
            var hasUser = await _userManager.FindByIdAsync(userId.ToString()!);

            if (hasUser == null)
            {
                ModelState.AddModelError(String.Empty, "Kullanýcý bulunamamýþtýr.");
                var resetPasswordErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = resetPasswordErrors });
            }

            IdentityResult result = await _userManager.ResetPasswordAsync(hasUser, token.ToString()!, request.Password);
            if (!result.Succeeded)
            {
                var signUpErrors = result.Errors.Select(x => x.Description).ToList();
                return Json(new { success = false, errors = signUpErrors });
            }

            return Json(new { success = true});
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel request)
        {


            if (!ModelState.IsValid)
            {
                var signInErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = signInErrors });
            }

            var hasUser = await _userManager.FindByEmailAsync(request.Email);

            if (hasUser == null || !(await _userManager.CheckPasswordAsync(hasUser, request.Password)))
            {
                ModelState.AddModelError(string.Empty, "Email veya þifre yanlýþ");
                var signInErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();

                return Json(new { success = false, errors = signInErrors });
            }
            var signInViewModelRememberMe = request.RememberMe;
            var signInResult = await _signInManager.PasswordSignInAsync(hasUser, request.Password, request.RememberMe, true);

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "3 dakika boyunca giriþ yapamazsýnýz.");
                var signInErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = signInErrors });
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email veya þifre yanlýþ");
                var signInErrors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                return Json(new { success = false, errors = signInErrors });
            }

            // Baþarýlý giriþ durumu
            return Json(new { success = true });
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
