using AspNetCoreIdentityApp.Web.CustomValidations;
using BootcampApp.Core.Models;
using BootcampApp.Repository;
using BootcampApp.Service.Validations;
using BootcampApp.Web.Localizations;
using Microsoft.AspNetCore.Identity;

namespace BootcampApp.Web.Extenisons
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
             {
                 opt.TokenLifespan = TimeSpan.FromHours(2);
             });



            services.AddIdentity<User, Role>(options =>
              {

                  options.User.RequireUniqueEmail = true;
                  options.User.AllowedUserNameCharacters = "abcdefghijklmnoprstuvwxyz1234567890_";

                  options.Password.RequiredLength = 6;
                  options.Password.RequireNonAlphanumeric = false;
                  options.Password.RequireLowercase = true;
                  options.Password.RequireUppercase = false;
                  options.Password.RequireDigit = true;


                  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                  options.Lockout.MaxFailedAccessAttempts = 3;




              })
               .AddUserValidator<UserValidator>()
               .AddPasswordValidator<PasswordValidator>()
               .AddErrorDescriber<LocalizationIdentityErrorDescriber>()
               .AddDefaultTokenProviders()
               .AddEntityFrameworkStores<BootcampAppDbContext>();

        }
    }
}
