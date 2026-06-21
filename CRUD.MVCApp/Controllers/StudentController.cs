using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace CRUD.MVCApp.Controllers
{
    public class StudentController : Controller
    {
        [ActionName("Index")]
        public async Task<IActionResult> StudentIndex()
        {
            SqlConnectionStringBuilder connectionstring = new SqlConnectionStringBuilder() { 
            DataSource = "DELL",
            InitialCatalog = "Revision",
            UserID = "sa",
            Password = "root",
            TrustServerCertificate = true
            };

            using IDbConnection db = new SqlConnection(connectionstring.ConnectionString);
            db.Open();
            var lst =await db.QueryAsync<WalletModel>("select * from Students");
            return View("StudentIndex", lst.ToList());
        }
    }

    public class WalletModel { 
        public string Roll_No { get; set; }
        public string Name { get; set; }
    }

}
