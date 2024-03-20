using Microsoft.AspNetCore.Identity;

namespace BootcampApp.Web.Localizations
{
    public class LocalizationIdentityErrorDescriber : IdentityErrorDescriber
    {

        public override IdentityError DuplicateUserName(string userName)
        {

            return new() { Code = "DuplicateUserName", Description = $"{userName} daha önce başka bir kullanıcı tarafından alınmıştır" };

            //return base.DuplicateUserName(userName);
        }


        public override IdentityError DuplicateEmail(string email)
        {

            return new() { Code = "DuplicateEmail", Description = $"{email} daha önce başka bir kullanıcı tarafından alınmıştır" };

        }

        public override IdentityError PasswordTooShort(int length)
        {


            return new() { Code = "PasswordTooShort", Description = $"Şifre en az 6 karakterli olmalıdır" };

        }
        public override IdentityError InvalidEmail(string? email)
        {
            return new() { Code = "InvalidEmail", Description = $"Geçersiz Email Formatı" };
        }

        public override IdentityError PasswordMismatch()
        {
            return new() { Code = "PasswordMismatch", Description = $"Hatalı Şifre veya kullanıcı adı" };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new() { Code = "PasswordRequiresDigit", Description = $"Şifre rakam içermelidir" };
        }
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new() { Code = "PasswordRequiresNonAlphanumeric", Description = $"Şifre özel karakter içermelidir(. * / vb)" };
        }

        public override IdentityError InvalidUserName(string? userName)
        {
            return new() { Code = "InvalidUserName", Description = $"Kulanıcı Adı Uygun değildir" };
        }


    }
}
