using Databased.Blog.Models;

namespace BlogApi.Model
{
    public class CreateResponseModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ReadResponseModelById
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public int? Id { get; set; }
        public string Caption { get; set; }
        public DateTime Date { get; set; }
    }

    public class ReadResponseModelByList
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public List<TblBlog> Data { get; set; }
    }

    public class UpdateResponse
    {
        public bool Success { get; set; }   
        public string Message { get; set; }
    }

}
