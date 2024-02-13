using PersonalFinance.Api;
using PersonalFinance.Services;
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
    .AddUserSecrets<Program>() 
    .Build();

var fxRatesProviderSettings = conf.GetSection("fxRatesProviderSettings");
var fxRatesConnectionStrings = fxRatesProviderSettings.GetSection("FxRatesConnectionStrings");

var fxRatesProviderResolver = new FxRatesProviderResolver();
fxRatesProviderResolver.Add("CSV", new CsvRateProvider(fxRatesConnectionStrings["CsvFilePath"], ';', 30000));
fxRatesProviderResolver.Add("MySql", new MySqlRateProvider(fxRatesConnectionStrings["MySqlConnectionString"]));
fxRatesProviderResolver.Add("MSSQL", new SqlServerRateProvider(fxRatesConnectionStrings["SqlServerConnectionString"]));

builder.Services.AddSingleton(fxRatesProviderResolver);

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
