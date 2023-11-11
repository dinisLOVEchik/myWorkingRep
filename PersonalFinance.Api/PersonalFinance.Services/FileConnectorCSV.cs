using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Services
{
    public class FileConnectorCSV
    {
        public string connector() 
        {
            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build().GetSection("Path").GetSection("FilePath").Value;
        }
    }
}
