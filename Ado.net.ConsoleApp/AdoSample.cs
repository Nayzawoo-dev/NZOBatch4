using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.net.ConsoleApp
{
    public class AdoSample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;
        public AdoSample(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public void Read()
        {
            string query = "select * from Tbl_Product";
            SqlConnection _connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            _connection.Close();

            if (dt.Rows.Count is 0) return;

            foreach (DataRow item in dt.Rows)
            {
                Console.WriteLine($"{item["ProductName"].ToString()} : {item["Price"]} : {item["Quantity"]}");
            }
        }

        public void ReadById()
        {
            string query = "select * from Tbl_Product where Id = 6";
            SqlConnection _connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            _connection.Close();
            if (dt.Rows.Count is 0) return;
            DataRow item = dt.Rows[0];
            Console.WriteLine($"{item["ProductName"].ToString()} : {item["Price"]} : {item["Quantity"]}");
        }

        public void Edit()
        {
            SqlConnection _connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            string query = "select * from Tbl_Product where Id = 6";
            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            _connection.Close();
            if (dt.Rows.Count is 0)
            {
                Console.WriteLine("Product Not Found");
                return;
            }

            DataRow item = dt.Rows[0];
            Console.WriteLine($"{item["ProductName"].ToString()} : {item["Price"].ToString()} : {item["Quantity"].ToString()}");


            Console.WriteLine("1. Update Product");
            Console.WriteLine("2. Delete Product");
            Console.WriteLine("3. Exit");
            Console.Write("Choose Your Option");
            int opt = Convert.ToInt32(Console.ReadLine());
            switch (opt)
            {
                case 1: Update(); break;
                case 2: Delete(); break;
                case 3:
                default: break;
            }
        }

        public void Update()
        {
            string query = @"UPDATE [dbo].[Tbl_Product]
   SET [ProductName] = 'Banana'
      ,[Price] = 1000
      ,[Quantity] = 10
      ,[IsDelete] = 0
 WHERE Id = 3";

            SqlConnection _connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataAdapter dt = new SqlDataAdapter(cmd);
            var res = cmd.ExecuteNonQuery();
            _connection.Close();
            string message = res is 0 ? "Update Failed" : "Update Successfully";
            Console.WriteLine(message);
        }

        public void Delete()
        {
            string query = @"DELETE FROM [dbo].[Tbl_Product]
      WHERE Id = 6";
            SqlConnection _connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            var res = cmd.ExecuteNonQuery();
            _connection.Close();
            string message = res is 0 ? "Delete Failed" : "Delete Successful";
            Console.WriteLine(message);
        }

        public void Create()
        {
            string query = @"INSERT INTO [dbo].[Tbl_Product]
           ([ProductName]
           ,[Price]
           ,[Quantity]
           ,[IsDelete])
     VALUES
           ('Pineapple'
           ,1000
           ,100
           ,0)";
            SqlConnection _connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            _connection.Open();
            SqlCommand cmd = new SqlCommand(query, _connection);
            var res = cmd.ExecuteNonQuery();
            _connection.Close();
            string message = res is 0 ? "Insert Failed" : "Insert Successful";
            Console.WriteLine(message);
        }
    }
}
