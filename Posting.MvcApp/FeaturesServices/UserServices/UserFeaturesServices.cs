using Posting.MvcApp.DatabaseServices;
using Posting.MvcApp.Models;

namespace Posting.MvcApp.FeaturesServices.UserServices;

public class UserFeaturesServices
{
    private readonly DapperServices _dapperServices;

    public UserFeaturesServices(DapperServices dapperServices)
    {
        _dapperServices = dapperServices;
    }

    public async Task<List<ResponseUserModel>> ReadUserAsync()
    {
        string query = "SELECT * FROM Tbl_Users";
        var result = await _dapperServices
            .QueryAsync<ResponseUserModel>(query);

        if (result.Count <= 0)
        {
            return new List<ResponseUserModel>
        {
            new ResponseUserModel
            {
                Message = "No User List!"
            }
        };
        }
        return result;
    }


    public async Task<ResponseModel> CreateUserAsync(RequestUserModel model)
    {
        model.created_at = DateTime.Now;
        string query = @"INSERT INTO [dbo].[Tbl_Users]
           ([roll_no]
           ,[username]
           ,[email]
           ,[password]
           ,[created_at])
     VALUES
           (@roll_no
           ,@username
           ,@email
           ,@password
           ,@created_at)";

        var result = await _dapperServices.ExecuteAsync(query, model);

        return new ResponseModel()
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Account Create Successful" : "Account Create Failed"
        };
    }

}

