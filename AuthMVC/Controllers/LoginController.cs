using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthMVC.Controllers;

public class LoginController : Controller
{
    public static List<UserInfo> _userinfo = new List<UserInfo>
    {
        new UserInfo
        {
            Username = "Admin",
            UserId = 1,
            Email = "admin@gmail.com",
            Password = "123456",
            Role = "Admin"
        },
        new UserInfo
        {
            Username = "User",
            UserId = 2,
            Email = "user@gmail.com",
            Password = "123456",
            Role = "User"
        }
    };
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> IndexAsync(LoginRequest request)
    {
        var user = _userinfo.FirstOrDefault(x => x.Email == request.Email && x.Password == request.Password);
        if(user is null)
        {
            TempData["Error"] = "Invalid email or password";
            return View();
        };

        var claims = new List<Claim>
        {
            new Claim("Username", user.Username),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = request.RememberMe,
            ExpiresUtc = request.RememberMe is true ? DateTimeOffset.UtcNow.AddHours(7) : DateTimeOffset.UtcNow.AddDays(1)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return Redirect("/Home");
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

public class UserInfo
{
    public string Username { get; set; }
    public int UserId { get; set; }
    public string Email { get; set; }

    public string Role { get; set; }

    public string Password { get; set; }
}
