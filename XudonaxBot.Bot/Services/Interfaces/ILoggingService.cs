using Discord;

namespace XudonaxBot.Bot.Services.Interfaces
{
    public interface ILoggingService
    {
        Task LogAsync(LogMessage message);
    }
}
