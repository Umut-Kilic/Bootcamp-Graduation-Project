namespace BootcampApp.Core.Models
{
    public class Category : BaseEntity
    {
        public string? Text { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
