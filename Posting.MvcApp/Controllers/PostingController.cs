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

        public IActionResult UserList()
        {
            List<UserModel> lst = _services.ReadUser();
            return View(lst);
        }
    }
}
