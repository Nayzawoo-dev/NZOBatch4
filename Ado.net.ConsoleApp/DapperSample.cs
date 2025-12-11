using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado.net.ConsoleApp
{
    public class DapperSample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;
        public DapperSample(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public void Read()
        {
            string query = "select * from Tbl_Product";
            using (IDbConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var res = connection.Query<Productdt>(query).ToList();
                if (res.Count > 0)
                {
                    Console.WriteLine("Product List");
                    foreach (var item in res)
                    {
                        Console.WriteLine($"{item.ProductName} : {item.Price} : {item.Quantity}");
                    }
                }
                var message = res.Count is 0 ? "Product Not Found" : string.Empty;
            }

            return;
        }

        public void ReadById()
        {
            string query = "select * from Tbl_Product where Id = 6";
            using (IDbConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var res = connection.Query<Productdt>(query).FirstOrDefault();
                if (res is not null)
                {
                    Console.WriteLine($"{res.ProductName} : {res.Price.ToString("n2")} : {res.Quantity.ToString("n0")}");
                }
                var message = res is null ? "Product Not Found" : string.Empty;
            }
            return;
        }

        public void Edit()
        {

            string query = "select * from Tbl_Product where Id = 6";
            using (IDbConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var res = connection.Query<Productdt>(query).FirstOrDefault();
                if (res is not null)
                {
                    Console.WriteLine($"{res.ProductName} : {res.Price.ToString("n2")} : {res.Quantity.ToString("n0")}");
                }
                else
                {
                    Console.WriteLine("Product Not Found");
                    return;
                }
            }

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
 WHERE Id = 6";
            using (IDbConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var res = connection.Execute(query);
                var message = res > 0 ? "Update Successful" : "Update Failed";
                Console.WriteLine(message);
            }
        }

        public void Delete()
        {
            string query = @"DELETE FROM [dbo].[Tbl_Product]
      WHERE Id = 6";
            using (IDbConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var res = connection.Execute(query);
                var message = res > 0 ? "Delete Successful" : "Update Failed";
                Console.WriteLine(message);
            }

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
            using (IDbConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var res = connection.Execute(query);
                var message = res > 0 ? "Insert Successful" : "Insert Failed";
                Console.WriteLine(message);
            }
        }
    }

    public class Productdt
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public bool IsDelete { get; set; }
    }
}

