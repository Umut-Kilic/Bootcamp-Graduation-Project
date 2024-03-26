namespace BootcampApp.Core.Models
{
    public enum CategoryColor
    {
        primary, danger, warning, success, secondary
    }
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Text { get; set; }
        public string Url { get; set; }
        public CategoryColor? Color { get; set; }
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
