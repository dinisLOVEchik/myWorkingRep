using System;
using System.IO;
using System.Timers;

namespace PersonalFinance.Services
{
    public class TimerElapsed
    {
        private readonly Timer _timer;
        private string[] _csvLines;
        private readonly string _csvFileName;

        public TimerElapsed(string[] csvLines, string csvFileName)
        {
            _csvLines = csvLines;
            _csvFileName = csvFileName;
            _timer = new Timer(30000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.Start();
        }

        private void OnTimerElapsed(object source, ElapsedEventArgs args)
        {
            string[]newCsvFile = File.ReadAllLines(_csvFileName);
            lock(_csvLines)
            {
                _csvLines = newCsvFile;
            }
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
