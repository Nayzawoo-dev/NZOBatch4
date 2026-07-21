using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _fluentemail;

        public EmailController(IEmailService fluentemail)
        {
            _fluentemail = fluentemail;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] EmailRequest request)
        {
            var response = await _fluentemail.SendEmailAsync(request.To, request.Subject, request.Body);
            return Ok(response);
        }
    }

    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
