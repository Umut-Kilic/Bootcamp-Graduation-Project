using BootcampApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootcampApp.ViewComponents
{
    public class CategoryMenu : ViewComponent{
        private ICategoryService _categoryService;

        public CategoryMenu(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(){
            return View( await _categoryService.GetAll().ToListAsync());
        }
    }
}