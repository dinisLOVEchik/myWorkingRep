using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Tests
{
    internal class MockExchangeRatesGenerationService
    {
        const int MIN_VALUE = 1;
        const int MAX_VALUE = 1000;
        public List<ExcangeRate> MockExchangeRatesGenerator(List<string> isoCodesOfRates)
        {
            List<ExcangeRate> result = new List<ExcangeRate>();

            for (int currency1 = 0; currency1 < isoCodesOfRates.Count; currency1++)
            {
                string x = isoCodesOfRates[currency1];
                for (int currency2 = 0; currency2 < isoCodesOfRates.Count; currency2++)
                {
                    string y = isoCodesOfRates[currency2];

                    if (result.Count == 0 && !isoCodesOfRates[currency1].Equals(isoCodesOfRates[currency2]))
                    {
                        result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], new Random().Next(MIN_VALUE, MAX_VALUE)));
                    }
                    else if (result.Count == 0)
                    {
                        result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], MIN_VALUE));
                    }
                    else
                    {
                        if (isoCodesOfRates[currency1].Equals(isoCodesOfRates[currency2]))
                        {
                            result.Add(new ExcangeRate(isoCodesOfRates[currency2], isoCodesOfRates[currency1], MIN_VALUE));
                        }
                        else
                        {
                            int count = 0;
                            foreach (ExcangeRate rate in result.ToList())
                            {
                                count++;
                                if (isoCodesOfRates[currency1].Equals(rate.getValue2()) && isoCodesOfRates[currency2].Equals(rate.getValue1()))
                                {
                                    result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], Math.Round(MIN_VALUE / rate.getRate(), 7)));
                                    break;
                                }
                                else if (count == result.Count)
                                {
                                    result.Add(new ExcangeRate(isoCodesOfRates[currency1], isoCodesOfRates[currency2], new Random().Next(MIN_VALUE, MAX_VALUE)));
                                    break;
                                }
                            }
                        }

                    }
                };
            };
            return result;
        }
    }

    public class ExcangeRate
    {
        private string currency1;
        private string currency2;
        private decimal rate;

        public ExcangeRate(string value1, string value2, decimal rate)
        {
            this.currency1 = value1;
            this.currency2 = value2;
            this.rate = rate;
        }
        public string getValue1()
        {
            return currency1;
        }
        public string getValue2()
        {
            return currency2;
        }
        public decimal getRate()
        {
            return rate;
        }
        public void setValue1(string value)
        {
            this.currency1 = value;
        }
        public void setValue2(string value)
        {
            this.currency2 = value;
        }
        public void setRate(decimal value)
        {
            this.rate = value;
        }
    }
}
