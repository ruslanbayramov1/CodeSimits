namespace CodeSimits.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string receiver, string userName, string password, string token);
        Task SendForgotPasswordAsync(string receiver, string userName, string token);
    }
}
