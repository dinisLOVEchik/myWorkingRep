using System;
using System.IO;
using System.Timers;

namespace PersonalFinance.Services
{
    public class CsvRateProvider : IRateProvider
    {
        private readonly string _filename;
        private readonly Timer _timer;
        private string[] _csvLines;

        public CsvRateProvider(string filename)
        {
            _filename = filename;
            _timer = new Timer(30000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        public decimal GetRate(string currencyFrom, string currencyTo)
        {
            lock (this)
            {
                _csvLines = File.ReadAllLines(_filename);
            }

            for (int i = 0; i < _csvLines.Length; i++)
            {
                string[] rowData = _csvLines[i].Split(';');

                if (rowData[0] == currencyFrom && rowData[1] == currencyTo)
                {
                    return Decimal.Parse(rowData[2]);
                }
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
