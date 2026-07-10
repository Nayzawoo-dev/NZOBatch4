using Serilog;
using Serilog.Sinks.MSSqlServer;

string folderpatch = AppDomain.CurrentDomain.BaseDirectory;
string logFilePath = Path.Combine(folderpatch, "logs", "myapp.txt");

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
   .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Hour)
             .WriteTo
    .MSSqlServer(
        connectionString: "Server=DELL;Database=Revision;User ID=sa;Password=root;TrustServerCertificate=true;",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "Table_LogEvents", AutoCreateSqlTable = true })
            .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog();


    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

