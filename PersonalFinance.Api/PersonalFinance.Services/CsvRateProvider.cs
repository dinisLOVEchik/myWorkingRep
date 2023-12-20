using System;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace PersonalFinance.Services
{
    public class CsvRateProvider : IRateProvider
    {
        private readonly string _filename;
        private readonly System.Timers.Timer _timer;
        private string[] _csvLines;
        private readonly char _delimetr;
        private readonly Dictionary<string, Dictionary<string, decimal>> _rates;
        private readonly object _mutex;

        public CsvRateProvider(string filename, char delimetr, int updatePeriod)
        {
            _filename = filename;
            _delimetr = delimetr;
            _timer = new System.Timers.Timer(updatePeriod);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
            _rates = new Dictionary<string, Dictionary<string, decimal>>();
            _mutex = new object();
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            lock (_mutex)
            {
                if (_rates.Count == 0)
                {
                    PopulateRates();
                }
                if (_rates.ContainsKey(currencyFrom) && _rates.ContainsKey(currencyTo))
                {
                    return _rates[currencyFrom][currencyTo];
                }
                else
                {
                    throw new Exception($"There is a problem getting exchange rate between {currencyFrom}/{currencyTo}, your request can't be completed");
                }
            }
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            lock (_mutex)
            {
                _rates.Clear();
                PopulateRates();
            }
        }
        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        private void PopulateRates()
        {
            _csvLines = File.ReadAllLines(_filename);
            for (int i = 1; i < _csvLines.Length; i++)
            {
                string[] rows = _csvLines[i].Split(_delimetr);
                var currFrom = rows[0];
                var currTo = rows[1];
                var rate = Decimal.Parse(rows[2], CultureInfo.InvariantCulture);

                if (!_rates.ContainsKey(currFrom))
                {
                    _rates[currFrom] = new Dictionary<string, decimal>();
                }
                _rates[currFrom][currTo] = rate;
            }
        }
    }
}
