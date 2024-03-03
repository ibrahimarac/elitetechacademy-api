using Elitetech.Academy.Application;
using Elitetech.Academy.Application.Automapper;
using Elitetech.Academy.CrossCutting.IoC;
using Elitetech.Academy.Services.Api.Configurations;
using FluentValidation;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

//Logging
var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

builder.Logging.ClearProviders();
Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();

#region Register Services

//Register Controllers
builder.Services.AddControllers();

//Automapper
builder.Services.AddAutoMapper(typeof(DomainToDtoMapping), typeof(DtoToDomainMapping));

//Register other layers services
NativeInjectorBootStrapper.RegisterServices(builder.Services);

//Register Db
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Fluent Validation
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(ApplicationAssemblyInfo)));

#endregion

builder.Host.UseSerilog();

var app = builder.Build();

#region Register Middlawares

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

try
{
    Log.Logger.Information("Program Started...");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Exiting with error..");
}
finally
{
    Log.CloseAndFlush();
}

