using System;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Bump.Log
{
    public class ProdLogger : ILogger
    {
        private const string ErrorFile = "bump-errors.log";
        private const string LogsFile = "bump.log";

        public ProdLogger()
        {
            if (File.Exists(ErrorFile))
            {
                File.Delete(ErrorFile);
                File.Create(ErrorFile);
            }

            if (File.Exists(LogsFile))
            {
                File.Delete(LogsFile);
                File.Create(LogsFile);
            }
            
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var allFile = File.AppendText(LogsFile);
            allFile.WriteLine(formatter(state, exception));
            allFile.Close();

            if (logLevel != LogLevel.Error) return;
            
            var file = File.AppendText(ErrorFile);
            file.WriteLine(formatter(state, exception));
            file.Flush();
            file.Close();

            var originalColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($@"[{eventId.Id,2}: {logLevel,-12}]");

            Console.ForegroundColor = originalColor;
            Console.WriteLine(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state) => default;
    }
}