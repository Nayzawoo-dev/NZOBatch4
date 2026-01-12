// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using Dapper;
Console.WriteLine("Hello, World!");

string jsonfile = "MyanmarMonths.json";

var json = await File.ReadAllTextAsync(jsonfile);

var data = JsonConvert.DeserializeObject<MonthResponseModel>(json);

string connectionString = "Server=.;Database=Batch4.Database;User Id=sa;Password=sasa@123;TrustServerCertificate=True;";
using var connection = new SqlConnection(connectionString);

string sql = @"
INSERT INTO Tbl_Months
(MonthMm, MonthEn, FestivalMm, FestivalEn, [Description], Detail)
VALUES
(@MonthMm, @MonthEn, @FestivalMm, @FestivalEn, @Description, @Detail);
";

connection.Open();

foreach (var item in data.Tbl_Months)
{
    await connection.ExecuteAsync(sql, item);
}

Console.WriteLine("Insert Complete");

public class MonthResponseModel
{
    public Tbl_Months[] Tbl_Months { get; set; }
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
