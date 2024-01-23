using PersonalFinance.Api;
using PersonalFinance.Services;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(
    x=>
    {
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
    );
 //Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var conf = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true)
    .Build();

var fxRatesProviderSettings = conf.GetSection("fxRatesProviderSettings");
var fxRatesConnectionStrings = fxRatesProviderSettings.GetSection("FxRatesConnectionStrings");

if (fxRatesProviderSettings["FxRateProvider"] == "CSV")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new CsvRateProvider("./data/Output.csv", ';', 30000);
    });
}
else if (fxRatesProviderSettings["FxRateProvider"] == "MySql")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new MySqlRateProvider(fxRatesConnectionStrings["MySqlConnectionString"]);
    });
}
else if (fxRatesProviderSettings["FxRateProvider"] == "MSSQL")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new SqlServerRateProvider(fxRatesConnectionStrings["SqlServerConnectionString"]);
    });
}
else
{
    throw new InvalidOperationException("Invalid rate provider type");
}

builder.Services.AddTransient<CurrencyConverter>();
builder.Services.AddTransient<CurrencyValidator>();


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
