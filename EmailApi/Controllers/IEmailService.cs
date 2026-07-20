using FluentEmail.Core.Models;

namespace EmailApi.Controllers
{
    public interface IEmailService
    {
        Task<SendResponse> SendEmailAsync(string toEmail, string subject, string body);
    }
}