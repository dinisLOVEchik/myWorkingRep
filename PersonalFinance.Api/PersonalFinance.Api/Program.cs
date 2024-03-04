using PersonalFinance.Api;
using PersonalFinance.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var fxRatesProviderSettings = builder.Configuration.GetSection("fxRatesProviderSettings");
var fxRatesConnectionStrings = fxRatesProviderSettings.GetSection("FxRatesConnectionStrings");

var fxRatesProviderResolver = new FxRatesProviderResolver();
fxRatesProviderResolver.Add("CSV", new CsvRateProvider(fxRatesConnectionStrings["CsvFilePath"], ';', 30000));
fxRatesProviderResolver.Add("MySql", new MySqlRateProvider(fxRatesConnectionStrings["MySqlConnectionString"]));
fxRatesProviderResolver.Add("MSSQL", new SqlServerRateProvider(fxRatesConnectionStrings["SqlServerConnectionString"]));

builder.Services.AddSingleton(fxRatesProviderResolver);

builder.Services.AddTransient<CurrencyValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();