using System;
using MySql.Data.MySqlClient;

namespace PersonalFinance.Services
{
    public class MySqlRateProvider:IRateProvider
    {
        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            MySqlConnector mySqlConnector = new MySqlConnector();

            MySqlConnection connection = mySqlConnector.CreateConnection();

            string sql = "SELECT rate FROM rates WHERE curr1 = '" + currencyFrom + "' AND curr2 = '" + currencyTo + "'";

            MySqlCommand command = new MySqlCommand(sql, connection);

            connection.Open();

            string rate = command.ExecuteScalar().ToString();

            connection.Close();

            return Decimal.Parse(rate);
        }
    }
}
