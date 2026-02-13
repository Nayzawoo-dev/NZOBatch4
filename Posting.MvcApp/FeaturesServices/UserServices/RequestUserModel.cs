using Posting.MvcApp.Models;
namespace Posting.MvcApp.FeaturesServices.UserServices;

public class RequestUserModel : ResponseModel
{
    public string? roll_no { get; set; }
    public string? username { get; set; }
    public string? email { get; set; }

    public string? password { get; set; }

    public DateTime created_at { get; set; }
}
