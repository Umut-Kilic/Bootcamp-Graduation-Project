using BootcampApp.Core.Models;
using Microsoft.AspNetCore.Identity;

namespace BootcampApp.Service.Validations
{
    public class PasswordValidator : IPasswordValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string? password)
        {
            var errors = new List<IdentityError>();
            if (password!.ToLower().Contains(user.UserName!.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Code = "PasswordContainUserName",
                    Description = "Şifre kullanıcı adı içeremez",
                });
            }


            if (password!.ToLower().StartsWith("1234"))
            {
                errors.Add(new() { Code = "PasswordContain1234", Description = "Şifre alanı ardışık sayı içeremez" });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);

        }
    }
}
