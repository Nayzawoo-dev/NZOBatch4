using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace SqlServices
{
    public class AdoSqlServices
    {
        private readonly SqlConnectionStringBuilder _connection;
        public AdoSqlServices(SqlConnectionStringBuilder connection)
        {
            _connection = connection;
        }

        public DataTable Query(string query, List<SqlParameter> parameters = null!)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddRange(parameters.ToArray());
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            connection.Close();
            return dt;
        }

        public int Execute(string query, params SqlParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(_connection.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query,connection);
            cmd.Parameters.AddRange(parameters);
            int res = cmd.ExecuteNonQuery();
            connection.Close();
            return res;
        }

        public List<T> Query<T>(string query, params SqlParameter[] parameters)
        {
            SqlConnection conn = new SqlConnection(_connection.ConnectionString);   
            conn.Open();
            SqlCommand cmd = new SqlCommand(query,conn);
            cmd.Parameters.AddRange(parameters);
            SqlDataAdapter adt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adt.Fill(dt);
            conn.Close();
            string json = JsonConvert.SerializeObject(dt);
            var res = JsonConvert.DeserializeObject<List<T>>(json)!;
            return res;
        }
    }
}
