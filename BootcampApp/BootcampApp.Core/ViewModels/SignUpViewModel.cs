using System.ComponentModel.DataAnnotations;

namespace BootcampApp.Core.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpViewModel()
        { }

        public SignUpViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }
        [Required(ErrorMessage = "Kullanıcı adı alanı boş bırakılamaz.")]
        [MinLength(4, ErrorMessage = "Kullanıcı adı en az 4 karakter olabilir")]
        [Display(Name = "Kullanıcı Ad :")]
        public string UserName { get; set; } = null!;


        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;



        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Şifre :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string Password { get; set; } = null!;




        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Şifre aynı değildir.")]
        [Required(ErrorMessage = "Şifre tekrar alanı boş bırakılamaz")]
        [Display(Name = "Şifre Tekrar :")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string PasswordConfirm { get; set; } = null!;





        public bool PermissionAllow { get; set; }
    }
}
