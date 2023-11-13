using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.IO;

namespace PersonalFinance.Services
{
    public class MySqlConnector
    {
        private MySqlConnection _connection;
        public MySqlConnection CreateConnection()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = builder.Build();

            var MySQLConnectionString = _configuration.GetConnectionString("MySQLConnection");

            _connection = new MySqlConnection(MySQLConnectionString);

            return _connection;
        }
    }
}
