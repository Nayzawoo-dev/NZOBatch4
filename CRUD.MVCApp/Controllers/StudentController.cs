using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;

namespace CRUD.MVCApp.Controllers
{
    public class StudentController : Controller
    {
        SqlConnectionStringBuilder connectionstring = new SqlConnectionStringBuilder()
        {
            DataSource = "DELL",
            InitialCatalog = "Revision",
            UserID = "sa",
            Password = "root",
            TrustServerCertificate = true
        };
        [ActionName("Index")]
        public async Task<IActionResult> StudentIndex()
        {
            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();
            var lst = await db.QueryAsync<StudentModel>("select * from Students");
            return View("StudentIndex", lst.ToList());
        }

        [ActionName("Create")]
        public IActionResult StudentCreate()
        {
            return View("CreateIndex");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> StudentCreate(StudentModel requestmodel)
        {
            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();
            string query = @"INSERT INTO [dbo].[Students]
           ([Roll_No]
           ,[Name])
     VALUES
           (@Roll_No
           ,@Name
           )";
            var result = await db.ExecuteAsync(query, requestmodel);
            bool isSuccess = result > 0;
            string message = isSuccess ? "Success" : "Failed";
            TempData["isSuccess"] = isSuccess;
            TempData["message"] = message;

            var task1 = Run();
            var task2 = Run();
            await Task.WhenAll(task1, task2);

            return RedirectToAction("Index");
        }

        public async Task Run()
        {

        }
    }

    public class StudentModel
    {
        public string Roll_No { get; set; }
        public string Name { get; set; }
    }

}
