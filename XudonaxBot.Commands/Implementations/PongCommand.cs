using Discord.WebSocket;
using XudonaxBot.Commands.Interfaces;

namespace XudonaxBot.Commands.Implementations
{
    public class PongCommand : ISlashCommand
    {
        public string Name => "pong";

        public string Description => "Send a pong command to the bot";

        public async Task HandleAsync(SocketSlashCommand command)
        {
            await command.RespondAsync("Pong!");
        }
    }
}
