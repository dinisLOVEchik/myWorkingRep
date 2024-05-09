using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace PersonalFinance.Services
{
    public class SqlServerRateProvider : IRateProvider
    {
        private readonly string _connectionString;
        private readonly ILogger<SqlServerRateProvider> _logger;

        public SqlServerRateProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            string sql = "SELECT rate FROM rates WHERE curr1 = @currencyFrom AND curr2 = @currencyTo";

            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                using (var sqlCommand = new SqlCommand(sql, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@currencyFrom", currencyFrom);
                    sqlCommand.Parameters.AddWithValue("@currencyTo", currencyTo);
                    sqlConnection.Open();
                    try
                    {
                        return Decimal.Parse(sqlCommand.ExecuteScalar().ToString());
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
            using var sqlConnection = new SqlConnection(_connectionString);
            using var sqlCommand = new SqlCommand(sql, sqlConnection);
            sqlConnection.Open();
            using var reader = sqlCommand.ExecuteReader();
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
                using var sqlConnection = new SqlConnection(_connectionString);
                sqlConnection.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
