using Serilog;
using Serilog.Sinks.MSSqlServer;

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Hour)
             .WriteTo
    .MSSqlServer(
        connectionString: "Server=DELL;Database=Revision;User ID=sa;Password=root;TrustServerCertificate=true;",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Table_LogEvents" , AutoCreateSqlTable = true })
            .CreateLogger();

Log.Information("Hello, world!");

int a = 10, b = 0;
try
{
    Log.Debug("Dividing {A} by {B}", a, b);
    Console.WriteLine(a / b);
}
catch (Exception ex)
{
    Log.Error(ex, "Something went wrong");
}
finally
{
    await Log.CloseAndFlushAsync();
}