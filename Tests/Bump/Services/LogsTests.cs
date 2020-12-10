using System;
using System.IO;
using System.Linq;
using Bump.Services.Log;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Tests.Bump.Services {

    public class LogsTests {

        private const string Category = "Test";
        private const string Message = "Message";
        private readonly ILoggerProvider Provider = new LoggerProvider();
        private ILogger Logger;

        [SetUp]
        public void SetUp() {
            Logger = Provider.CreateLogger( Category );
        }

        [Test]
        public void ProviderTest() {
            Assert.IsInstanceOf< ProdLogger >( Logger );
        }

        [Test]
        public void LoggerErrorLogTest() {

            var errorsOldTime = File.GetLastWriteTime( ProdLogger.ErrorFile );
            var logsOldTime = File.GetLastWriteTime( ProdLogger.LogsFile );

            var oldOut = Console.Out;
            using var consoleCapture = new MemoryStream();
            using var consoleWriter = new StreamWriter( consoleCapture );

            Console.SetOut( consoleWriter );

            Logger.Log(
                LogLevel.Error ,
                new Exception( Message ) ,
                Message
            );

            Console.SetOut( oldOut );
            consoleWriter.Flush();
            
            var errorsNewTime = File.GetLastWriteTime( ProdLogger.ErrorFile );
            var logsNewTime = File.GetLastWriteTime( ProdLogger.LogsFile );

            Assert.Greater( errorsNewTime , errorsOldTime );
            Assert.Greater( logsNewTime , logsOldTime );
            Assert.Greater( consoleCapture.Length , 0 );
        }

        [Test]
        public void LoggerInfoLogTest() {

            var logsOldTime = File.GetLastWriteTime( ProdLogger.LogsFile );

            Logger.Log(
                LogLevel.Information ,
                Message
            );

            var logsNewTime = File.GetLastWriteTime( ProdLogger.LogsFile );

            Assert.Greater( logsNewTime , logsOldTime );
        }

        [Test]
        public void EnabledLevelsTest() {
            var levelsEnabled = Enum.GetValues( typeof( LogLevel ) )
                .Cast< LogLevel >()
                .Select( it => Logger.IsEnabled( it ) );

            Assert.That( levelsEnabled.All( it => it ) );
        }

    }

}