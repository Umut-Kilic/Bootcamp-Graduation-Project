using BootcampApp.Core.Models;
using BootcampApp.Web.Areas.Admin.Models;
using BootcampApp.Web.Extenisons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootcampApp.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {

            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Name = x.Name!
            }).ToListAsync();



            return View(roles);
        }



        public IActionResult RoleCreate()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            var result = await _roleManager.CreateAsync(new Role() { Name = request.Name });

            if (!result.Succeeded)
            {

                ModelState.AddModelErrorList(result.Errors);
                return View();
            }


            TempData["SuccessMessage"] = "Rol oluşturulmuştur.";
            return RedirectToAction(nameof(RolesController.Index));



        }



        public async Task<IActionResult> RoleUpdate(string id)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(id);

            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek rol bulunamamıştır.");
            }


            return View(new RoleUpdateViewModel() { Id = roleToUpdate.Id, Name = roleToUpdate!.Name! });
        }


        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request)
        {

            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);

            if (roleToUpdate == null)
            {
                throw new Exception("Güncellenecek rol bulunamamıştır.");
            }

            roleToUpdate.Name = request.Name;

            await _roleManager.UpdateAsync(roleToUpdate);


            ViewData["SuccessMessage"] = "Rol bilgisi güncellenmiştir";

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> RoleDelete(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);

            if (roleToDelete == null)
            {
                throw new Exception("Silinecek rol bulunamamıştır.");
            }

            var result = await _roleManager.DeleteAsync(roleToDelete);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());
            }

            TempData["SuccessMessage"] = "Rol silinmiştir";
            return RedirectToAction(nameof(RolesController.Index));

        }
        public async Task<IActionResult> AssignRoleToUser(string id)
        {
          
            ViewBag.userId = id;
            if (id == null)            {                return RedirectToAction("Index");            }            var user = await _userManager.FindByIdAsync(id);            if (user != null)            {                ViewBag.Roles = await _roleManager.Roles.Select(i => i.Name).ToListAsync();                return View(new AssignRoleToUserViewModel
                {                    Id = user.Id,                    Name = user.UserName,                    SelectedRoles = await _userManager.GetRolesAsync(user)                });            }

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleToUser(string userId, AssignRoleToUserViewModel request)
        {
            if (userId == null)
            {
                return RedirectToAction("Index");
            }

            var userToAssignRoles = await _userManager.FindByIdAsync(userId);

            if (userToAssignRoles != null)
            {
                var userRoles = await _userManager.GetRolesAsync(userToAssignRoles);
                await _userManager.RemoveFromRolesAsync(userToAssignRoles, userRoles.ToArray());

                if (request.SelectedRoles != null && request.SelectedRoles.Any())
                {
                    foreach (var role in request.SelectedRoles)
                    {
                        await _userManager.AddToRoleAsync(userToAssignRoles, role);

                    }
                }
            }

            return RedirectToAction(nameof(HomeController.UserList), "Home");
        }

    }
}