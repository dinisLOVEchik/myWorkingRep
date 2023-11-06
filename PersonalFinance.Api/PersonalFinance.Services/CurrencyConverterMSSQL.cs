using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace PersonalFinance.Services
{
    public class CurrencyConverterMSSQL
    {
        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            decimal res = 0;

            SqlConnection conn = new SqlConnection(@"Data Source=localhost,1433;Initial Catalog=RatesBase;Integrated Security=true");

            conn.Open();

            string sql = "SELECT * FROM rates";

            SqlCommand command = new SqlCommand(sql, conn);

            SqlDataReader reader = command.ExecuteReader();

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
