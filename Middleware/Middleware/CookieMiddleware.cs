using System.Runtime.CompilerServices;

namespace Middleware.Middleware
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var useremail = context.Request.Cookies["UserEmail"];
            if (!allowedUrls.Any(x => x == context.Request.Path))
            {
                if (string.IsNullOrEmpty(useremail))
                {
                    context.Response.Redirect("/Login/Index");
                }

            }

            await _next(context);

        }

        private string[] allowedUrls = { "/", "/Login/Index", "/Login" };
    }
}
