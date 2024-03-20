using BootcampApp.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BootcampApp.Web.Controllers
{
    [Authorize]
    public class MemberController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public MemberController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();

        }
    }
}
