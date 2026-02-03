using Posting.MvcApp.DatabaseServices;

namespace Posting.MvcApp.FeaturesServices;

public class UserFeaturesServices
{
    private readonly DapperServices _dapperServices;

    public UserFeaturesServices(DapperServices dapperServices)
    {
        _dapperServices = dapperServices;
    }

    public async Task<List<UserModel>> ReadUserAsync()
    {
        string query = "select * from Tbl_Users";
        List<UserModel> result = await _dapperServices.QueryAsync<UserModel>(query);
        return result;
    }
}

