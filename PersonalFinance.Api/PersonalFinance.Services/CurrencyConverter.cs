using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PersonalFinance.Services
{
    public class CurrencyConverter
    {
        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            string filePath = builder.Build().GetSection("Path").GetSection("FilePath").Value;

            string[] csvLines = File.ReadAllLines(filePath);
            decimal res = 0;

            for (int i = 0; i < csvLines.Length; i++)
            {
                string[] rowData = csvLines[i].Split(';');

                if (rowData[0] == currencyFrom && rowData[1] == currencyTo)
                {
                    res = Decimal.Parse(rowData[2]) * amount;
                }
            }
            return res;
        }
    }
    
}
