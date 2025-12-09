using Microsoft.Data.SqlClient;
using System.Data;

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "Batch4.Database";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";
sqlConnectionStringBuilder.TrustServerCertificate = true;

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
connection.Open();

//Read
#region

string query = "select * from Tbl_Product";
SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adt = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adt.Fill(dt);

#endregion

//Insert
#region

string query = @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[Price]
           ,[Quantity]
           ,[IsDelete])
     VALUES
           ('Watermalon'
           ,1000
           ,30
           ,0)";
SqlCommand cmd = new SqlCommand(query, connection);
int res = cmd.ExecuteNonQuery();

string message = res is 1 ? "Insert Successful" : "Insert Failed";

#endregion

//Update
#region

string query = @"UPDATE [dbo].[Tbl_Product]
   SET [ProductName] = 'Apple'
 WHERE Id = 1";
SqlCommand cmd = new SqlCommand(query, connection);
int res = cmd.ExecuteNonQuery();

string message = res is 1 ? "Update Successful" : "Update Failed";

#endregion

//Delete
#region

string query = @"DELETE FROM [dbo].[Tbl_Product]
      WHERE Id = 1";
SqlCommand cmd = new SqlCommand(query, connection);
int res = cmd.ExecuteNonQuery();

string message = res is 1 ? "Delete Successful" : "Delete Failed";

#endregion

connection.Close();

Console.WriteLine("Product List!");

foreach (DataRow dr in dt.Rows)
{
    Console.WriteLine($"{dr["ProductName"].ToString()} {dr["Price"]} {dr["Quantity"]}");
}
