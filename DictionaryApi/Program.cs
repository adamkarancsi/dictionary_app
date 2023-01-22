using DictionaryDataAccess;
using DictionaryBusinessLogic;

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
builder.Services.AddDataAccessDependencies(configuration);
builder.Services.AddBusinessLogicDependencies();
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