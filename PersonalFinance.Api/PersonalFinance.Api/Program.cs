using PersonalFinance.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(
    x=>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
    );
 //Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conf =  builder.Configuration.AddJsonFile("appsettings.json");

var rateProviderSettings = builder.Configuration.GetSection("RateProviders");

// Регистрация сервисов в зависимости от конфигурации
/*if (rateProviderSettings["CsvRateProvider"] == "CSV")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new CsvRateProvider("./data/Output.csv", ';', 30000);
    });
}*/
if (rateProviderSettings["MySqlRateProvider"] == "MySql")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new MySqlRateProvider("MySQLConnection");
    });
}
else if (rateProviderSettings["SqlServerRateProvider"] == "MS-SQL")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new SqlServerRateProvider("MSSQLConnection");
    });
}
else
{
    // Обработка ошибки - не найден подходящий провайдер
}

builder.Services.AddTransient<CurrencyConverter>();


//if (conf[$"RateProviders:{rateProvider.GetType().Name}"])
//builder.Services.AddTransient<IRateProvider, SqlServerRateProvider>(sql => new SqlServerRateProvider("MSSQLConnection"));
//builder.Services.AddTransient<IRateProvider, MySqlRateProvider>(mySql => new MySqlRateProvider("MySQLConnection"));
//builder.Services.AddTransient<IRateProvider, CsvRateProvider>(csv => new CsvRateProvider("./data/Output.csv", ';', 30000));
//builder.Services.AddTransient<CurrencyConverter>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapGet("api/server/ping", ()=> "pong");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
