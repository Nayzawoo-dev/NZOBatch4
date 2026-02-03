using Posting.MvcApp.DatabaseServices;
namespace Posting.MvcApp.FeaturesServices;

public class UserFeaturesServices
{
    private readonly DapperServices _dapperServices;

    public UserFeaturesServices(DapperServices dapperServices)
    {
        _dapperServices = dapperServices;
    }

    public List<UserModel> ReadUser()
    {
        string query = "select * from Tbl_Users";
        var res = _dapperServices.Query<UserModel>(query);
        return res;
    }
}
