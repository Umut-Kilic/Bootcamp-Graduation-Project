namespace BootcampApp.Core.Models
{
    public class Category 
    {
        public int CategoryId{ get; set; }
        public string? Text { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
