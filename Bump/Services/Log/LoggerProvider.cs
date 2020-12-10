using Microsoft.Extensions.Logging;

namespace Bump.Services.Log {

    public class LoggerProvider : ILoggerProvider {

        private readonly ILogger _logger = new ProdLogger();

        public void Dispose() { }

        public ILogger CreateLogger( string categoryName ) {
            return _logger;
        }

    }

}