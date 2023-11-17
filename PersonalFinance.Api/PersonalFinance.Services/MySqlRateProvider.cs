using Microsoft.Extensions.Configuration;
using System;
using MySql.Data.MySqlClient;
using System.IO;

namespace PersonalFinance.Services
{
    public class MySqlRateProvider : IRateProvider
    {
        private readonly string _servername;

        public MySqlRateProvider(string servername)
        {
            _servername = servername;
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            string sql = "SELECT rate FROM rates WHERE curr1 = '" + currencyFrom + "' AND curr2 = '" + currencyTo + "'";

            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = builder.Build();

            var connectionString = _configuration.GetConnectionString(_servername);

            MySqlConnection mySqlConnection = new MySqlConnection(connectionString);

            MySqlCommand mySqlCommand = new MySqlCommand(sql, mySqlConnection);

            mySqlConnection.Open();

            string rate = mySqlCommand.ExecuteScalar().ToString();

            mySqlConnection.Close();

            return Decimal.Parse(rate);
        }
    }
}
