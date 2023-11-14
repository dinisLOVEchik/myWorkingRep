using MySql.Data.MySqlClient;

namespace PersonalFinance.Services
{
    public class MySqlConnector
    {
        private readonly string _connectionString;

        public MySqlConnector(string connectionString)
        {
            _connectionString = connectionString;
        }
        public MySqlConnection Connection()
        {
            ConnectionStringCreator mySqlConnector = new ConnectionStringCreator(_connectionString);
            MySqlConnection mySqlConnection = new MySqlConnection(mySqlConnector.GetConnectionString());
            return mySqlConnection;
        }

        public string GetRateValue(MySqlConnection connection, string sqlCommand)
        {
            MySqlCommand mySqlCommand = new MySqlCommand(sqlCommand, connection);
            return mySqlCommand.ExecuteScalar().ToString();
        }
    }
}
