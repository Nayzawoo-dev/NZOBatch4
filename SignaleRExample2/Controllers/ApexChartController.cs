using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using SignaleRExample2.Hubs;
using System.Data;

namespace SignaleRExample2.Controllers
{
    public class ApexChartController : Controller
    {
        private readonly SqlConnectionStringBuilder connectionstring;
        private readonly IHubContext<ChatHub> _hubContext;

        public ApexChartController(IConfiguration configuration, IHubContext<ChatHub> hubContext)
        {
            connectionstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("DbConnection"));
            _hubContext = hubContext;
        }


        public async Task<IActionResult> PieChart()
        {
            List<StudentModel> model = new List<StudentModel>();
            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            model = (await db.QueryAsync<StudentModel>("select Age, Name from Students")).ToList();
            return View(model);
        }

        [ActionName("Create")]
        public IActionResult PieCreate()
        {
            return View("PieCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> PieSave(StudentModel requestModel)
        {

            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();

            string query = @"INSERT INTO [dbo].[Students]
           ([Name]
           ,[Age])
     VALUES
           (@Name
           ,@Age
           )";
            db.Execute(query, requestModel);
            var model = db.Query<StudentModel>("select Age, Name from Students").ToList();
            var _series = model.Select(x => x.Age).ToList()!;
            var _labels = model.Select(x => x.Name).ToList()!;
            _hubContext.Clients.All.SendAsync("UpdateChart",_series,_labels,model.Count);
            return RedirectToAction("Create");
        }
    }

    public class StudentModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}