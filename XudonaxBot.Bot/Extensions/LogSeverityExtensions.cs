using Discord;
using Microsoft.Extensions.Logging;

namespace XudonaxBot.Bot.Extensions
{
    internal static class LogSeverityExtensions
    {
        public static LogLevel AsLogLevel(this LogSeverity severity) => severity switch
        {
            LogSeverity.Critical => LogLevel.Critical,
            LogSeverity.Error => LogLevel.Error,
            LogSeverity.Warning => LogLevel.Warning,
            LogSeverity.Info => LogLevel.Information,
            LogSeverity.Debug => LogLevel.Debug,
            LogSeverity.Verbose => LogLevel.Trace,
            _ => LogLevel.None
        };
    }
}
