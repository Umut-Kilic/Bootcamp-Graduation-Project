using BootcampApp.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BootcampApp.Web.ViewComponents
{
    public class SignUp:ViewComponent
    {
        

        public SignUp(IPostRepository postRepository)
        {
           
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}

