using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZOB4Database.Shared
{
    public class DapperServices
    {
        private readonly SqlConnectionStringBuilder _connection;
        public DapperServices(SqlConnectionStringBuilder connection)
        {
            _connection = connection;
        }

        public List<T> Query<T>(string query, object? parameters)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            var res = connection.Query<T>(query, parameters).ToList();
            connection.Close();
            return res;
        }
        public int Execute(string query, object? parameters) 
        { 
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);  
            connection.Open();
            var res = connection.Execute(query, parameters);
            connection.Close();
            return res;
        }
    }
}
