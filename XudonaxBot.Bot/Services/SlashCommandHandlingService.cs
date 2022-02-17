using Discord.WebSocket;
using XudonaxBot.Bot.Repositories.Interfaces;
using XudonaxBot.Bot.Services.Interfaces;
using XudonaxBot.Commands.Interfaces;

namespace XudonaxBot.Bot.Services
{
    internal class SlashCommandHandlingService : ISlashCommandHandlingService
    {
        private readonly IReadOnlyRepository<ISlashCommand> _commandRepository;

        public SlashCommandHandlingService(IReadOnlyRepository<ISlashCommand> commandRepository)
        {
            _commandRepository = commandRepository;
        }

        public async Task Handle(SocketSlashCommand command)
        {
            var name = command.CommandName;

            await _commandRepository.Get(name).HandleAsync(command);
        }
    }
}
