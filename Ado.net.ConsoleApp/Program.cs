
using Ado.net.ConsoleApp;
using Microsoft.Data.SqlClient;

Console.WriteLine("---Mini Pos---");

DapperSample ado = new DapperSample(new SqlConnectionStringBuilder
{
    DataSource = ".",
    InitialCatalog = "Batch4.Database",
    UserID = "sa",
    Password = "sasa@123",
    TrustServerCertificate = true,
});
before:
Console.WriteLine("1. Look Product List");
Console.WriteLine("2. Edit Product");
Console.WriteLine("3. Insert Product");
Console.WriteLine("4. Exit");

Console.Write("Choose Your Option : ");
var opt = Convert.ToInt32(Console.ReadLine());
switch (opt)
{
    case 1: ado.Read(); goto before;
    case 2: ado.Edit(); goto before;
    case 3: ado.Create(); goto before;
    case 4:
    default: break;
}
