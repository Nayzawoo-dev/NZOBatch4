using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AjaxApp.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly SqlConnectionStringBuilder connectionstring;

        public StudentController(IConfiguration configuration)
        {
            connectionstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("DbConnection"));
        }

        [ActionName("Index")]
        public IActionResult StudentIndex()
        {
            return View("StudentIndex");
        }

        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> StudentList()
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
                db.Open();
                var lst = await db.QueryAsync<StudentModel>("select * from Students");
                bool isSuccess = lst is not null;
                string message = isSuccess ? "Success" : "Failed";
                return Json(new { Message = message, isSuccess = isSuccess, Data = lst });
            }
            catch (Exception ex)
            {
                return Json(new { Message = "Failed", isSuccess = false });
            }
        }


        [ActionName("Create")]
        public IActionResult StudentCreate()
        {
            return View("StudentCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> StudentSave(StudentModel requestModel)
        {

            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();
            string query = string.Empty;
            if (requestModel.Id == 0)
            {
                query = @"INSERT INTO [dbo].[Students]
           ([Roll_No]
           ,[Name])
     VALUES
           (@Roll_No
           ,@Name
           )";
            }
            else
            {
                query = @"UPDATE [dbo].[Students]
   SET [Roll_No] = @Roll_No
      ,[Name] = @Name
 WHERE @Id = Id";
            }
            var result = await db.ExecuteAsync(query, requestModel);
            bool isSuccess = result > 0;
            string message = isSuccess ? "Success" : "Failed";

            return Json(new { IsSuccess = isSuccess, Message = message });

        }



        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> StudentDelete(StudentModel requestModel)
        {

            try
            {
                using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
                db.Open();

                string query = @"DELETE FROM [dbo].[Students] WHERE Id = @Id";

                var result = await db.ExecuteAsync(query, requestModel);
                bool isSuccess = result > 0;
                string message = isSuccess ? "Success" : "Failed";

                return Json(new { IsSuccess = isSuccess, Message = message });
            }
            catch (Exception ex)
            {

                return Json(new { IsSuccess = false, Message = "An error occurred while trying to delete the record." });
            }

        }

        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> StudentEdit(StudentModel requestModel)
        {

            try
            {
                using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
                db.Open();

                string query = @"SELECT * FROM [dbo].[Students] WHERE Id = @Id";

                var result = await db.QueryFirstOrDefaultAsync<StudentModel>(query, requestModel);
                bool isSuccess = result is not null;
                string message = isSuccess ? "Success" : "Failed";

                return Json(new { IsSuccess = isSuccess, Message = message, Data = result });
            }
            catch (Exception ex)
            {

                return Json(new { IsSuccess = false, Message = "An error occurred while trying to delete the record." });
            }

        }

    }

    public class StudentModel
    {
        public int Id { get; set; }
        public string Roll_No { get; set; }
        public string Name { get; set; }
    }
}
