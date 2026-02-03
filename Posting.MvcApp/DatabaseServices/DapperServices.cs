using Dapper;
using Microsoft.Data.SqlClient;

namespace Posting.MvcApp.DatabaseServices
{
    public class DapperServices
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnectionStringBuilder _connection;
        public DapperServices(IConfiguration configuration)
        {
            _configuration = configuration; 
            _connection = new SqlConnectionStringBuilder(_configuration.GetConnectionString("Database"));
        }

        public List<T> Query<T>(string query, object? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            var res = connection.Query<T>(query, parameters).ToList();
            connection.Close();
            return res;
        }

        public int Execute(string query, object? parameters = null)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            var res = connection.Execute(query, parameters);
            connection.Close();
            return res;
        }


    }
}

