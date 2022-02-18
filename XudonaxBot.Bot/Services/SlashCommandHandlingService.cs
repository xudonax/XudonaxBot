using Discord.WebSocket;
using XudonaxBot.Bot.Services.Interfaces;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.DAL.Repositories.Interfaces;

namespace XudonaxBot.Bot.Services
{
    internal class SlashCommandHandlingService : ISlashCommandHandlingService
    {
        private readonly IReadOnlyRepository<ISlashCommand, string> _commandRepository;

        public SlashCommandHandlingService(IReadOnlyRepository<ISlashCommand, string> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task Handle(SocketSlashCommand command)
        {
            var name = command.CommandName;
            var implementation = _commandRepository.Get(name);

            if (implementation == null) return;

            await implementation.HandleAsync(command);
        }
    }
}
