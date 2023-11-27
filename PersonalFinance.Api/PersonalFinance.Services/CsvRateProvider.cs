using System;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Globalization;

namespace PersonalFinance.Services
{
    public class CsvRateProvider : IRateProvider
    {
        private readonly string _filename;
        private readonly Timer _timer;
        private string[] _csvLines;
        private readonly char _delimetr;
        private readonly Dictionary<string, Dictionary<string, decimal>> _rates;

        public CsvRateProvider(string filename, char delimetr)
        {
            _filename = filename;
            _delimetr = delimetr;
            _timer = new Timer(30000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
            _rates = new Dictionary<string, Dictionary<string, decimal>>();
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
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
            if (_rates.ContainsKey(currencyFrom) && _rates.ContainsKey(currencyTo))
            {
                return _rates[currencyFrom][currencyTo];
            }
            return 0;
        }
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            string[] newCsvLines = File.ReadAllLines(_filename);

            lock (this)
            {
                _csvLines = newCsvLines;
            }
        }
        public void Dispose()
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
