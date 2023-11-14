using Microsoft.Extensions.Configuration;
using System.IO;

namespace PersonalFinance.Services
{
    public class ConnectionStringCreator
    {
        private readonly string _serverName;

        public ConnectionStringCreator(string serverName)
        {
            _serverName = serverName;
        }
        public string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = builder.Build();

            var connectionString = _configuration.GetConnectionString(_serverName);

            return connectionString;
        }
    }
}
