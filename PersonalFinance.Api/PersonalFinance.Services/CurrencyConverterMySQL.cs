using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace PersonalFinance.Services
{
    public class CurrencyConverterMySQL
    {
        string connStr = "server=localhost;user=root;database=rates_db;password=00400040;";

        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            decimal res = 0;

            MySqlConnection conn = new MySqlConnection(connStr);

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

            return res;
        }
    }
}
