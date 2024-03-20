namespace BootcampApp.Core.Services
{
    public interface IEmailService
    {
        Task SendResetPasswordEmailAsync(string resetPaswwordEmailLink, string ToEmail);
    }
}
