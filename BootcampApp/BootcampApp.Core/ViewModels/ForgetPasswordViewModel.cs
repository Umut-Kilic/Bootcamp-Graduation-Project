﻿using System.ComponentModel.DataAnnotations;

namespace BootcampApp.Core.ViewModels
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Display(Name = "Email :")]
        public string? Email { get; set; } = null!;

    }
}
