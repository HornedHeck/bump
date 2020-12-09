using System;
using Bump.Utils.Log;
using Microsoft.Extensions.Logging;

namespace Bump.Log
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly ILogger _logger = new ProdLogger();

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _logger;
        }
    }
}