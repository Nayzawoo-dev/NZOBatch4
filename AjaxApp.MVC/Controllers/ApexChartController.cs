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

            
            var rawData = await db.QueryAsync("select Age, Name from Students");

           
            var model = new ApexChartPieChartViewModel
            {
                Age = rawData.Select(x => (int)x.Age).ToList(),
                Labels = rawData.Select(x => (string)x.Name).ToList()
            };

           
            return View(model);
        }
    }

    public class ApexChartPieChartViewModel
    {
        public List<int> Age { get; set; }

        public List<string> Labels { get; set; }
    }
}