using BootcampApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampApp.Core.ViewModels
{
    public class IndexViewModel
    {
        public List<Post> Posts { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public SliderViewModel SliderViewModel { get; set; }
        public SignInViewModel SignInViewModel { get; set; }
        public SignUpViewModel SignUpViewModel { get; set; }
        public ResetPasswordViewModel ResetPasswordViewModel { get; set; }
        public ForgetPasswordViewModel ForgetPasswordViewModel { get; set; }
    }
}
