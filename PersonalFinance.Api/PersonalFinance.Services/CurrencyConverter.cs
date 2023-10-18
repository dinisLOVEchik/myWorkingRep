using System;
using System.Collections.Generic;
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
        String _fileName = "../PersonalFinance.Api/data/Output.csv";
        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            string[] csvLines = System.IO.File.ReadAllLines("D:/WindowsProgs/myWorkingRep/PersonalFinance.Api/PersonalFinance.Api/data/Output.csv");
            decimal res = 0;

            for (int i = 0; i < csvLines.Length; i++)
            {
                string[] rowData = csvLines[i].Split(',');

                if (rowData[0] == currencyFrom && rowData[1] == currencyTo)
                {
                    res = Decimal.Parse(rowData[2]) * amount;
                }
            }
            return res;
        }
    }
    
}
