using BootcampApp.Core.Models;
using BootcampApp.Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootcampApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UserList()
        {
            var userList = await _userManager.Users.ToListAsync();

            var userViewModelList = userList.Select(x => new UserViewModel()
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.UserName
            }).ToList();

            return View(userViewModelList);
        }
    }
}
