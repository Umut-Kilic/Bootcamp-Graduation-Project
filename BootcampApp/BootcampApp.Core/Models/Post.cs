namespace BootcampApp.Core.Models
{
    public class Post : BaseEntity
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Image { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
