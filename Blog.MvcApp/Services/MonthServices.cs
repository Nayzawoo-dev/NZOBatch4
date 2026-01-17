using Blog.MvcApp.ModelCollection;
using Database.MvcApp.Models;

namespace Blog.MvcApp.Services;

public class MonthServices : IMonthServices
{
    private readonly AppDbContext _db;
    public MonthServices(AppDbContext db)
    {
        _db = db;
    }

    public MonthsListResponseModel MonthsList()
    {
        try
        {
            var MonthList = _db.TblMonths.ToList();
            List<MonthsModel> model = new List<MonthsModel>();
            foreach (var item in MonthList)
            {
                model.Add(new MonthsModel
                {
                    Id = item.Id,
                    MonthMm = item.MonthMm,
                    FestivalMm = item.FestivalMm,
                    Description = item.Description,
                    Detail = item.Detail,
                });
            }
            MonthsListResponseModel responseModel = new MonthsListResponseModel()
            {
                Success = model is not null,
                Message = model is not null ? "Month List" : "Month Not Found",
                Data = model is not null ? model : new List<MonthsModel>()
            };


            return responseModel;
        }
        catch (Exception ex)
        {
            MonthsListResponseModel responseModel = new MonthsListResponseModel()
            {
                Success = false,
                Message = ex.ToString(),
                Data = new List<MonthsModel>()
            };
            return responseModel;
        }
    }
}
