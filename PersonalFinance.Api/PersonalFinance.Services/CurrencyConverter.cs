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
        private readonly IRateProvider rateProvider;

        public CurrencyConverter(IRateProvider rateProvider)
        {
            this.rateProvider = rateProvider;
        }

        public decimal Convert(string currencyFrom, string currencyTo, decimal amount)
        {
            var rate = rateProvider.GetRate(currencyFrom, currencyTo);
            return rate * amount;
        }
    }
    
}
