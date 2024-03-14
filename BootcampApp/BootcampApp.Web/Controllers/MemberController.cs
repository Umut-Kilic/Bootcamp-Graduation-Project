using Microsoft.AspNetCore.Mvc;

namespace BootcampApp.Web.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
