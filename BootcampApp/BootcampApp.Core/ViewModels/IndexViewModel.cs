using BootcampApp.Core.Models;

namespace BootcampApp.Core.ViewModels
{
    public class IndexViewModel
    {
        public List<Post>? Posts { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public SliderViewModel? SliderViewModel { get; set; }

    }
}
