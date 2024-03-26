using BootcampApp.Core.Models;

namespace BootcampApp.Core.ViewModels
{
    public class PostsViewModel
    {
        public List<Post> Posts { get; set; }= new List<Post>();
        public User?  User{ get; set; }

    }
}
