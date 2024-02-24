using Elitetech.Academy.Application.Automapper;
using Elitetech.Academy.CrossCutting.IoC;
using Elitetech.Academy.Services.Api.Configurations;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

Log.Logger.Information("Program Started...");

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();
