using Microsoft.Extensions.Logging;
using System;

namespace Dotnet2020.Api.Diagnostics
{
    static class Log
    {
        private static readonly Action<ILogger, string, Exception> startingSearchLogger =
            LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(5000, nameof(Log)),
                "Starting search query from {path}");

        private static readonly Action<ILogger, string, Exception> finishingSearchLogger =
            LoggerMessage.Define<string>(
                LogLevel.Information,
                new EventId(5001, nameof(Log)),
                "Finishing search query from {path}.");

        private static readonly Action<ILogger, string, Exception> errorSearchLogger =
            LoggerMessage.Define<string>(
                LogLevel.Error,
                new EventId(5002, nameof(Log)),
                "Failed query from {path}.");

        public static void StartingSearchLogger(this ILogger logger, string path)
        {
            startingSearchLogger(logger, path, null);
        }

        public static void FinishingSearchLogger(this ILogger logger, string path)
        {
            finishingSearchLogger(logger, path, null);
        }

        public static void ErrorSearchLogger(this ILogger logger, string path, Exception exception)
        {
            errorSearchLogger(logger, path, exception);
        }
    }
}
