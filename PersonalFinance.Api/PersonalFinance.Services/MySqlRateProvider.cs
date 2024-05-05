using Microsoft.Extensions.Configuration;
using System;
using MySql.Data.MySqlClient;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace PersonalFinance.Services
{
    public class MySqlRateProvider : IRateProvider
    {
        private readonly string _connectionString;
        private readonly ILogger<MySqlRateProvider> _logger;

        public MySqlRateProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            string sql = "SELECT rate FROM rates WHERE curr1 = @currencyFrom AND curr2 = @currencyTo";

            using (var mySqlConnection = new MySqlConnection(_connectionString))
            {
                using (var mySqlCommand = new MySqlCommand(sql, mySqlConnection))
                {
                    mySqlCommand.Parameters.AddWithValue("@currencyFrom", currencyFrom);
                    mySqlCommand.Parameters.AddWithValue("@currencyTo", currencyTo);
                    mySqlConnection.Open();
                    try
                    {
                        return Decimal.Parse(mySqlCommand.ExecuteScalar().ToString());
                    }
                    catch (Exception ex) when (ex is ArgumentNullException || ex is FormatException)
                    {
                        _logger.LogError($"Invalid data for the request: {sql}{Environment.NewLine} " +
                                $"the data: {ex.Data["currencyFrom"] = currencyFrom}{Environment.NewLine}{ex.Data["currencyTo"] = currencyTo}");
                    }
                }
            }
            return 0;
        }
        public RateResponse[] GetAll()
        {
            return new RateResponse[0];
        }
    }
}
