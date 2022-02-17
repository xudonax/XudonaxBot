using Discord.WebSocket;

namespace XudonaxBot.Bot.Services.Interfaces
{
    public interface ISlashCommandHandlingService
    {
        Task Handle(SocketSlashCommand command);
    }
}
