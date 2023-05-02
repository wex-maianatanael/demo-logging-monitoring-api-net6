using Demo.Api.Configuration;
using Demo.Api.CustomMiddlewares;
using Demo.Infra.Repository.Context;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
//builder.Logging.AddJsonConsole();
//builder.Logging.AddDebug();

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig
        .ReadFrom.Configuration(context.Configuration)
        .WriteTo.Console()
        .Enrich.WithExceptionDetails()
        .Enrich.FromLogContext()
        .Enrich.With<ActivityEnricher>()
        .WriteTo.EventCollector(@"http://localhost:8088", "c6a495bb-187d-4ee6-89e4-b29c08b1980f");
});

builder.Services.AddOpenTelemetryTracing(b =>
{
    b.SetResourceBuilder(
        ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
    .AddAspNetCoreInstrumentation()
    .AddEntityFrameworkCoreInstrumentation()
    .AddOtlpExporter(options => { options.Endpoint = new Uri("http://localhost:4317"); });
});

builder.Services.AddProblemDetails(options =>
{
    options.IncludeExceptionDetails = (context, ex) => false;
    options.OnBeforeWriteDetails = (context, details) =>
    {
        if(details.Status == 500)
        {
            details.Detail = "An error occurred in our API. Use the trace id when contacting us.";
        }
    };
    options.Rethrow<DbUpdateException>();
    options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
}); // using hellang lib

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddDatabaseConfig();
builder.AddDependencyInjectionResolver();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<BankDbContext>();

var app = builder.Build();

app.UseMiddleware<CriticalExceptionMiddleware>();
app.UseProblemDetails(); // using hellang middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("health").AllowAnonymous();

app.Run();
