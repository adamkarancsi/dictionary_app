using DictionaryBusinessLogic.Localization;
using DictionaryDataAccess;
using DictionaryDataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .Build();

builder.Services.AddCors(p =>
    p.AddPolicy(
        "CorsPolicy",
        corsBuilder => corsBuilder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader()
    )
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument();
builder.Services.AddDbContext<DictionaryDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("DictionaryApp")!));
builder.Services.AddScoped<ILocalizationRepository, LocalizationRepository>();
builder.Services.AddControllers();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();