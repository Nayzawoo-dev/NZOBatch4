using Dapper;
using Microsoft.Data.SqlClient;

public class MonthService : IMonthService
{
    private readonly string _connectionString;

    public MonthService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DbConnection")!;
    }

    private SqlConnection GetConnection()
        => new SqlConnection(_connectionString);

    public async Task<Tbl_Months> GetByIdAsync(int id)
    {
        using var connection = GetConnection();

        string sql = "SELECT * FROM Tbl_Months WHERE Id = @Id";

        return await connection.QueryFirstOrDefaultAsync<Tbl_Months>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Tbl_Months>> SearchByMonthAsync(string keyword)
    {
        using var connection = GetConnection();

        string sql = @"
SELECT * FROM Tbl_Months
WHERE MonthMm LIKE '%' + @Keyword + '%'
   OR MonthEn LIKE '%' + @Keyword + '%'
";

        return await connection.QueryAsync<Tbl_Months>(sql, new { Keyword = keyword });
    }   
}
