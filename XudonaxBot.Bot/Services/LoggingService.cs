using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using XudonaxBot.Bot.Extensions;
using XudonaxBot.Bot.Services.Interfaces;

namespace XudonaxBot.Bot.Services
{
    internal class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public Task LogAsync(LogMessage message)
        {
            var logLevel = message.Severity.AsLogLevel();

            if (message.Exception is CommandException cmdException)
            {
                _logger.Log(logLevel, cmdException, "[Command/{Severity}] {CommandName} failed to execute in {Channel}", message.Severity, cmdException.Command.Aliases[0], cmdException.Context.Channel);
            }
            else if (message.Exception is not null)
            {
                _logger.Log(logLevel, message.Exception, "[Command/{Severity}] Exception caught in {Source}", message.Severity, message.Source);
            }
            else
            {
                _logger.Log(logLevel, "[General/{Severity}] {message}", message.Severity, message);
            }

            return Task.CompletedTask;
        }
    }
}
