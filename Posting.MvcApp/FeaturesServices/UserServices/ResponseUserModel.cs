using Posting.MvcApp.Models;

namespace Posting.MvcApp.FeaturesServices.UserServices
{
    public class ResponseUserModel : ResponseModel
    {
        public string? roll_no { get; set; }
        public string? username { get; set; }
        public string? email { get; set; }
    }
}
