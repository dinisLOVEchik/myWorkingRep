using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace PersonalFinance.Services
{
    public class CsvRateProvider : IRateProvider
    {
        private readonly string _filename;

        public CsvRateProvider(string filename)
        {
            _filename = filename;
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            string[] csvLines = File.ReadAllLines(_filename);

            for (int i = 0; i < csvLines.Length; i++)
            {
                string[] rowData = csvLines[i].Split(';');

                if (rowData[0] == currencyFrom && rowData[1] == currencyTo)
                {
                    return Decimal.Parse(rowData[2]);
                }
            }
            return 0;
        }
    }
}
