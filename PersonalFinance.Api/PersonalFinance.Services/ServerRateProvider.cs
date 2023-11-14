using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;

namespace PersonalFinance.Services
{
    public class ServerRateProvider:IRateProvider
    {
        private readonly string _connectionString;

        public ServerRateProvider(string connectionString)
        {
            _connectionString = connectionString;
        }
        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            string sql = "SELECT rate FROM rates WHERE curr1 = '" + currencyFrom + "' AND curr2 = '" + currencyTo + "'";

            string rate = "";

            if (_connectionString == "MySqlConnection")
            {
                MySqlConnector mySqlConnector = new MySqlConnector(_connectionString);
                MySqlConnection mySqlConnection = mySqlConnector.Connection();
                mySqlConnection.Open();

                rate = mySqlConnector.GetRateValue(mySqlConnection, sql);

                mySqlConnection.Close();
            }
            else if (_connectionString == "MSSQLConnection")
            {
                SqlConnector sqlConnector = new SqlConnector(_connectionString);
                SqlConnection sqlConnection = sqlConnector.Connection();
                sqlConnection.Open();

                rate = sqlConnector.GetRateValue(sqlConnection, sql);

                sqlConnection.Close();
            }

            return Decimal.Parse(rate);
        }
    }
}
