using MessagingMicroservice;
using BreganUtils.ProjectMonitor;
using EmailMicroservice;
using Hangfire;
using Hangfire.Dashboard.Dark;
using Hangfire.PostgreSql;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Async(x => x.File("Logs/log.log", retainedFileCountLimit: 7, rollingInterval: RollingInterval.Day)).WriteTo.Console().CreateLogger();
Log.Information("Logger Setup");

AppConfig.LoadConfig();

#if DEBUG
ProjectMonitorConfig.SetupMonitor("debug", AppConfig.ProjectMonitorKey);
#else
ProjectMonitorConfig.SetupMonitor("release", AppConfig.ProjectMonitorKey);
#endif

ProjectMonitorCommon.ReportProjectUp("Messaging Microservice");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
JobStorage.Current = new PostgreSqlStorage(AppConfig.HFConnectionString, new PostgreSqlStorageOptions { SchemaName = "messagingmicroservice" });

builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(AppConfig.HFConnectionString, new PostgreSqlStorageOptions { SchemaName = "messagingmicroservice" })
        .UseDarkDashboard()
        );

builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

HangfireJobs.SetupHangfireJobs();

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