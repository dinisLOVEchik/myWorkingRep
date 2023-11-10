using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using MySql.Data;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace PersonalFinance.Services
{
    public class CurrencyConverterMySQL
    {
        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration _configuration = builder.Build();

            var MySQLConnectionString = _configuration.GetConnectionString("MySQLConnection");

            decimal res = 0;

            MySqlConnection conn = new MySqlConnection(MySQLConnectionString);

            conn.Open();

            string sql = "SELECT * FROM rates";

            MySqlCommand command = new MySqlCommand(sql, conn);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                if (reader[0].ToString() == currencyFrom && reader[1].ToString() == currencyTo)
                {
                    res = Decimal.Parse(reader[2].ToString()) * amount;
                }
            }

            reader.Close();

            conn.Close();

            return res;
        }
    }
}
