using Microsoft.AspNetCore.Identity;

namespace BootcampApp.Core.Models
{
    public class User : IdentityUser
    {
        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public string? Picture { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender? Gender { get; set; }
    }
}

