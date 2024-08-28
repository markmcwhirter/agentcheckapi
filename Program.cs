using FastEndpoints;

using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Serilog;

namespace AgentCheckApi;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
        .Build();

        var sequrl = configuration.GetValue<string>("seq");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Enrich.WithThreadId()
            .Enrich.WithProperty("Application", "AgentCheckApi")
            .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production")
            .Enrich.WithProperty("ThreadName", Thread.CurrentThread.Name ?? "Unnamed Thread")
            .Enrich.WithProcessId()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithClientIp()
            .Enrich.WithRequestHeader("User-Agent")
            .WriteTo.File("logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true)
            .WriteTo.Seq(sequrl)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
        builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddFastEndpoints();

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("corspolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services
        .AddHealthChecks()
            .AddMySql(
                connectionString: configuration["ConnectionStrings:DefaultConnection"],
                healthQuery: "SELECT 1;",
                name: "sql",
                failureStatus: HealthStatus.Degraded,
                tags: new string[] { "db", "sql", "mysql" });

        builder.Services.AddHealthChecksUI(opt =>
        {
            opt.SetEvaluationTimeInSeconds(10); //time in seconds between check    
            opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks    
            opt.SetApiMaxActiveRequests(1); //api requests concurrency    
            opt.AddHealthCheckEndpoint("feedback api", "/api/health"); //map health check api    

        })
        .AddInMemoryStorage();


        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AgentCheckApi.Data.AgentCheckContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            options.EnableSensitiveDataLogging();
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        builder.Host.UseSerilog(Log.Logger);

        var app = builder.Build();

        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true) // allow any origin
            .AllowCredentials()); // allow credentials

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseExceptionHandler();

        app.UseFastEndpoints(c =>
        {
            c.Versioning.Prefix = "v";
        });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapHealthChecks("/api/health", new HealthCheckOptions()
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        app.UseHealthChecksUI(delegate (Options options)
        {
            options.UIPath = "/healthcheck-ui";
            //options.AddCustomStylesheet("./HealthCheck/Custom.css");

        });

        app.UseAuthorization();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("api/v1/weatherforecast", (HttpContext httpContext) =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = summaries[Random.Shared.Next(summaries.Length)]
                })
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        //app.UseExceptionHandlingMiddleware();
        app.Run();
    }
}
