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

//builder.Services.AddTransient<IRateProvider, SqlServerRateProvider>(sql => new SqlServerRateProvider("MSSQLConnection"));
//builder.Services.AddTransient<IRateProvider, MySqlRateProvider>(mySql => new MySqlRateProvider("MySQLConnection"));
builder.Services.AddTransient<IRateProvider, CsvRateProvider>(csv => new CsvRateProvider("./data/Output.csv", ';', 30000));
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
