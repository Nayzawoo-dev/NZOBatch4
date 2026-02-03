using Microsoft.AspNetCore.Mvc;
using Posting.MvcApp.FeaturesServices;

namespace Posting.MvcApp.Controllers
{
    public class PostingController : Controller
    {
        private readonly UserFeaturesServices _services;

        public PostingController(UserFeaturesServices services)
        {
            _services = services;
        }

        public async Task<IActionResult> UserList()
        {
            List<UserModel> lst = await _services.ReadUserAsync();
            return View(lst);
        }
    }
}
