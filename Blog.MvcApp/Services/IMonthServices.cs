using Blog.MvcApp.ModelCollection;

namespace Blog.MvcApp.Services
{
    public interface IMonthServices
    {
        MonthsListResponseModel MonthsList();
    }
}