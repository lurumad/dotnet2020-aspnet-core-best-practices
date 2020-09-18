using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace HighPerformanceLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<LoggerMessageVsLoggerExtensionMethods>();
            Console.Read();
        }
    }

    [MemoryDiagnoser]
    public class LoggerMessageVsLoggerExtensionMethods
    {
        private readonly ILogger<LoggerMessageVsLoggerExtensionMethods> logger;

        public LoggerMessageVsLoggerExtensionMethods()
        {
            logger = new LoggerFactory().CreateLogger<LoggerMessageVsLoggerExtensionMethods>();
        }

        [Benchmark]
        public void ExtensionMethods()
        {
            logger.LogInformation("High performance logging {Message}", "Hello World!");
        }

        [Benchmark]
        public void LoggerMessage()
        {
            logger.TestLogger("Hello World!");
        }
    }

    static class Log
    {
        private static readonly Action<ILogger, string, Exception> _testLogger =
            LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(5000, nameof(TestLogger)),
                "High performance logging {Message}");

        public static void TestLogger(this ILogger logger, string message)
        {
            _testLogger(logger, message, null);
        }
    }
}
