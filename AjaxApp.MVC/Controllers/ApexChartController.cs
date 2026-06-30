using AjaxApp.MVC.Controllers;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace SLHDotNetTrainingBatch1.MvcApp3.Controllers
{
    public class ApexChartController : Controller
    {
        private readonly SqlConnectionStringBuilder connectionstring;

        public ApexChartController(IConfiguration configuration)
        {
            connectionstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("DbConnection"));
        }

        public async Task<IActionResult> PieChart()
        {
            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);

            // 1. Query as dynamic to grab the raw database rows easily
            var rawData = await db.QueryAsync("select Age, Name from Students");

            // 2. Map the dynamic rows into your existing ViewModel's lists
            var model = new ApexChartPieChartViewModel
            {
                Age = rawData.Select(x => (int)x.Age).ToList(),
                Labels = rawData.Select(x => (string)x.Name).ToList()
            };

            // 3. Pass your populated view model to the View
            return View(model);
        }
    }

    public class ApexChartPieChartViewModel
    {
        public List<int> Age { get; set; }

        public List<string> Labels { get; set; }
    }
}