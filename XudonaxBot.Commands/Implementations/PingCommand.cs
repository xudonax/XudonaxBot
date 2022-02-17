using Discord.WebSocket;
using XudonaxBot.Commands.Interfaces;

namespace XudonaxBot.Commands
{
    public class PingCommand : ISlashCommand
    {
        public string Name => "ping";

        public string Description => "Send a ping command to the bot";

        public async Task HandleAsync(SocketSlashCommand command)
        {
            await command.RespondAsync("Pong!", ephemeral: true);
        }
    }
}
