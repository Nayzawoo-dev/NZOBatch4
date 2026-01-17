namespace Blog.MvcApp.ModelCollection
{
    public class MonthsListResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<MonthsModel> Data { get; set; } = new List<MonthsModel>();
    }
}
