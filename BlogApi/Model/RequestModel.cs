namespace BlogApi.Model
{
    public class CreateRequestModel
    {
        public string Caption { get; set; }
        public DateTime Date { get; set; }
    }

    public class UpdateRequest
    {
        public string Caption { get; set; }
    }
}
