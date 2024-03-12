namespace BootcampApp.Core.Models
{
    public class Comment : BaseEntity
    {
        public string? Text { get; set; }
        public DateTime PublishedDate { get; set; }

        public int PostId { get; set; }
        public int LikeCount { get; set; }
        public Post Post { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
