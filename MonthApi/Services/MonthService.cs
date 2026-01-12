using Dapper;
using System.Data.SqlClient;

public class MonthService : IMonthService
{
    private readonly string _connectionString;

    public MonthService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
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

    public class Tbl_Months
    {
        public int Id { get; set; }
        public string MonthMm { get; set; }
        public string MonthEn { get; set; }
        public string FestivalMm { get; set; }
        public string FestivalEn { get; set; }
        public string Description { get; set; }
        public string Detail { get; set; }
    }
}
