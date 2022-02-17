using Discord.WebSocket;

namespace XudonaxBot.Commands.Interfaces
{
    public interface ISlashCommand
    {
        string Name { get; }
        string Description { get; }

        Task HandleAsync(SocketSlashCommand command);
    }
}
