using BootcampApp.Core.Models;

namespace BootcampApp.Core.ViewModels
{
    public class PostViewModel
    {
        public List<Post> Posts { get; set; }
        public List<Category> Categories { get; set; }=new List<Category>();
        public SliderViewModel SliderViewModel { get; set; } 
    }
}
