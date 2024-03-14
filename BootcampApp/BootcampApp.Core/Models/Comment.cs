﻿namespace BootcampApp.Core.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? Text { get; set; }
        public DateTime PublishedDate { get; set; }
        public int LikeCount { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; } = null!;

        public string UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
