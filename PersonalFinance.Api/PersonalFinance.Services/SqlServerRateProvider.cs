using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Logging;

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
        public RateResponse[] GetAll()
        {
            return new RateResponse[0];
        }
    }
}
