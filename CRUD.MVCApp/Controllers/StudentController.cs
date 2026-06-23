using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;
using System.Security.Cryptography.X509Certificates;

namespace CRUD.MVCApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly SqlConnectionStringBuilder connectionstring;

        public StudentController(IConfiguration configuration)
        {
            connectionstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("DbConnection"));
        }

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

            //var task1 = Run();
            //var task2 = Run();
            //await Task.WhenAll(task1, task2);

            return RedirectToAction("Index");
        }

        //public async Task Run()
        //{

        //}

        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> StudentEdit(int Id)
        {
            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();
            string query = @"SELECT [Id]
      ,[Roll_No]
      ,[Name]
      ,[Age]
  FROM [dbo].[Students]
  Where Id=@Id";
            var lst = await db.QueryFirstOrDefaultAsync<StudentModel>(query, new StudentModel { Id = Id });
            if (lst is null)
            {
                TempData["isSuccess"] = false;
                TempData["message"] = "No Data Found";
                return RedirectToAction("Index");
            }
            return View("EditIndex", lst);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> StudentUpdate(StudentModel responseModel)
        {
            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();
            string query = @"UPDATE [dbo].[Students]
   SET [Roll_No] = @Roll_No
      ,[Name] = @Name
 WHERE Id = @Id";
            var lst = await db.ExecuteAsync(query, responseModel);
            bool isSuccess = lst > 0;
            string message = isSuccess ? "Success" : "Failed";
            TempData["isSuccess"] = isSuccess;
            TempData["message"] = message;
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> StudentDelete(int Id)
        {
            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();
            string query = @"DELETE FROM [dbo].[Students]
      WHERE Id=@Id";
            var lst = await db.ExecuteAsync(query, new { Id = Id });
            bool isSuccess = lst > 0;
            string message = isSuccess ? "Success" : "Failed";
            TempData["isSuccess"] = isSuccess;
            TempData["message"] = message;
            return RedirectToAction("Index");
        }


    }

    public class StudentModel
    {
        public int Id { get; set; }
        public string Roll_No { get; set; }
        public string Name { get; set; }
    }

}
