using BlogApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlogApi.Services
{
    public interface IBlogServices
    {
        Task<CreateResponseModel> CreateAsync([FromBody] CreateRequestModel model);
        Task<ReadResponseModelByList> ReadAsync();
        Task<ReadResponseModelById> ReadAsync(int id);
        Task<UpdateResponse> UpdateAsync(int? id, [FromBody] UpdateRequest model);
        Task<UpdateResponse> DeleteAsync(int? id);

    }
}