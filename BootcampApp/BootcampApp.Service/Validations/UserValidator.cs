
using BootcampApp.Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace AspNetCoreIdentityApp.Web.CustomValidations
{
    public class UserValidator : IUserValidator<User>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
        {
            var errors = new List<IdentityError>();
            var isDigit = int.TryParse(user.UserName![0].ToString(), out _);

            if (isDigit)
            {
                errors.Add(new() { Code = "UserNameContainFirstLetterDigit", Description = "Kullanıcı adının ilk karekteri sayısal bir karakter içeremez" });
            }


            if (Regex.IsMatch(user.UserName, "[İıÇçŞşĞğÜüÖö]"))
            {
                errors.Add(new() { Code = "UserNameContainTurkishLetter", Description = "Kullanıcı adında Türkçe karakterler kullanılamaz." });
            }
            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}