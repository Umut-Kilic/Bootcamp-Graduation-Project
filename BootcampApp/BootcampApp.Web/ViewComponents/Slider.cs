using BootcampApp.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BootcampApp.Web.ViewComponents
{
    public class Slider : ViewComponent
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public Slider(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliderImagePath = Path.Combine(_hostEnvironment.WebRootPath, "img", "sliderimages");
            var imageNames = Directory.GetFiles(sliderImagePath)
                                       .Select(Path.GetFileName)
                                       .ToList();
            var sliderViewModel = new SliderViewModel
            {
                Images = imageNames
            };
            return View(sliderViewModel);
        }
    }
}
