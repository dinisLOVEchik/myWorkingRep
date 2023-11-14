using System.Data.SqlClient;

namespace PersonalFinance.Services
{
    public class SqlConnector
    {
        private readonly string _connectionString;

        public SqlConnector(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection Connection()
        {
            ConnectionStringCreator sqlConnector = new ConnectionStringCreator(_connectionString);
            SqlConnection sqlConnection = new SqlConnection(sqlConnector.GetConnectionString());
            return sqlConnection;
        }

        public string GetRateValue(SqlConnection connection, string sqlCom)
        {
            SqlCommand sqlCommand = new SqlCommand(sqlCom, connection);
            return sqlCommand.ExecuteScalar().ToString();
        }
    }
}
