﻿using BootcampApp.Core.Models;

namespace BootcampApp.Core.ViewModels
{
    public class UserViewModel
    {
        public string? UserName { get; set; }
        public Gender? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PictureUrl { get; set; }
        public PostsViewModel? PostsViewModel { get; set; }
    }
}
