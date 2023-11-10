using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace PersonalFinance.Services
{
    public class CurrencyConverterMSSQL
    {
        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            decimal res = 0;

            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = builder.Build();

            var MySQLConnectionString = _configuration.GetConnectionString("MSSQLConnection");

            SqlConnection conn = new SqlConnection(MySQLConnectionString);

            conn.Open();

            string sql = "SELECT rate FROM rates WHERE curr1 = '" + currencyFrom + "' AND curr2 = '" + currencyTo + "'";

            SqlCommand command = new SqlCommand(sql, conn);

            string dig = command.ExecuteScalar().ToString();

            res = Decimal.Parse(dig) * amount;

            conn.Close();

            return res;
        }
    }
}
