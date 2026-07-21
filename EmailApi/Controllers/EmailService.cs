using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace EmailApi.Controllers
{
    public class EmailService : IEmailService
    {
        private IFluentEmail _fluentemail;

        public EmailService(IFluentEmail fluentemail)
        {
            _fluentemail = fluentemail;
        }

        public async Task<SendResponse> SendEmailAsync(string toEmail, string subject, string body)
        {
            SendResponse response = await _fluentemail
                .To(toEmail)
                .Subject(subject)
                .Body(body)
                .SendAsync();
            return response;
        }
    }
}
