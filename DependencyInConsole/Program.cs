using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Cryptography;
using System.Security.AccessControl;


var services = new ServiceCollection();

string FromEmail = "nayzaw927890@gmail.com";

services.AddScoped<TestService>();
services.AddScoped<Test2Service>();
services.AddScoped<EmailService>();

services.AddFluentEmail(FromEmail).AddSmtpSender("smtp.gmail.com", 587, FromEmail, "ciqa afdj fouv iffz");

var provider = services.BuildServiceProvider();
var emailService = provider.GetRequiredService<EmailService>();
var result = await emailService.SendEmailAsync("nayzawoo.ace@gmail.com", "OTP Code", $"Your OTP {RandomNumberGenerator.GetInt32(0, 999999).ToString("D6")}");

Console.WriteLine("Email sent successfully: " + result.Successful);

//var testservice = provider.GetRequiredService<Test2Service>();

//testservice.DoSomethingElse();


public class EmailService
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

public class TestService
{
    public void DoSomething()
    {
        Console.WriteLine("Doing something Please...");
    }
}

public class Test2Service
{
    private readonly TestService _testService;

    public Test2Service(TestService testService)
    {
        _testService = testService;
    }
    public void DoSomethingElse()
    {
        _testService.DoSomething();
    }
}