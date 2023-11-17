using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;

namespace PersonalFinance.Services
{
    public class SqlServerRateProvider : IRateProvider
    {
        private readonly string _servername;

        public SqlServerRateProvider(string servername)
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

            SqlConnection sqlConnection = new SqlConnection(connectionString);

            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

            sqlConnection.Open();

            string rate = sqlCommand.ExecuteScalar().ToString();

            sqlConnection.Close();

            return Decimal.Parse(rate);
        }
    }
}
