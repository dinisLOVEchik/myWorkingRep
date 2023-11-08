using System;
using System.Data.SqlClient;

namespace PersonalFinance.Services
{
    public class CurrencyConverterMSSQL
    {
        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            decimal res = 0;

            SqlConnection conn = new SqlConnection(@"Data Source=localhost,1433;Initial Catalog=RatesBase;User ID=SA;Password=Dinislam12345;");

            conn.Open();

            string sql = "SELECT rate FROM rates WHERE curr1 = '" + currencyFrom + "' AND curr2 = '" + currencyTo + "'";

            SqlCommand command = new SqlCommand(sql, conn);

            string dig = command.ExecuteScalar().ToString();

            res = Decimal.Parse(dig) * amount;

            conn.Close();

            return res;
        }
    }
}
