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
        public CurrencyExchangeRate[] GetAll()
        {
            List<CurrencyExchangeRate> listOfRows = new List<CurrencyExchangeRate>();
            string sql = "SELECT * FROM rates";
            using var mySqlConnection = new MySqlConnection(_connectionString);
            using var mySqlCommand = new MySqlCommand(sql, mySqlConnection);
            mySqlConnection.Open();
            using var reader = mySqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var currFrom = reader.GetString(0);
                var currTo = reader.GetString(1);
                var rate = reader.GetDecimal(2);
                listOfRows.Add(new CurrencyExchangeRate(currFrom, currTo, rate.ToString()));
                //дописал этот блок, чтобы от кол-ва данных swagger не сошел с ума
                if (listOfRows.Count == 10)
                    break;
            }
            return listOfRows.ToArray();
        }
        public bool IsAvailable()
        {
            try
            {
                using var mySqlConnection = new MySqlConnection(_connectionString);
                mySqlConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
