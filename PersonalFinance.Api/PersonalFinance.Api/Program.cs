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

var conf =  builder.Configuration.AddJsonFile("appsettings.json");

var rateProviderSettings = builder.Configuration.GetSection("RateProviders");

if (rateProviderSettings["CsvRateProvider"] == "CSV")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new CsvRateProvider("./data/Output.csv", ';', 30000);
    });
}
else if (rateProviderSettings["MySqlRateProvider"] == "MySql")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new MySqlRateProvider("server=localhost;user=root;database=rates_db;password=00400040;");
    });
}
else if (rateProviderSettings["SqlServerRateProvider"] == "MSSQL")
{
    builder.Services.AddTransient<IRateProvider>(provider =>
    {
        return new SqlServerRateProvider("Data Source=localhost,1433;Initial Catalog=RatesBase;User ID=SA;Password=Sabur05Din01;");
    });
}
builder.Services.AddTransient<CurrencyConverter>();


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
