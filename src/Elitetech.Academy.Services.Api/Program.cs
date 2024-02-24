using Elitetech.Academy.CrossCutting.IoC;
using Elitetech.Academy.Services.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

#region Register Services

//Register Controllers
builder.Services.AddControllers();

//Register other layers services
NativeInjectorBootStrapper.RegisterServices(builder.Services);

//Register Db
builder.Services.AddDatabaseConfiguration(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#endregion

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
