using Microsoft.Extensions.Configuration;
using System;
using MySql.Data.MySqlClient;
using System.IO;

namespace PersonalFinance.Services
{
    public class MySqlRateProvider : IRateProvider
    {
        private readonly string _connectionString;

        public MySqlRateProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            string sql = "SELECT rate FROM rates WHERE curr1 = '" + currencyFrom + "' AND curr2 = '" + currencyTo + "'";

            using (var mySqlConnection = new MySqlConnection(_connectionString))
            {
                using (var mySqlCommand = new MySqlCommand(sql, mySqlConnection))
                {
                    mySqlConnection.Open();

                    try
                    {
                        return Decimal.Parse(mySqlCommand.ExecuteScalar().ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
            return 0;
        }
    }
}
