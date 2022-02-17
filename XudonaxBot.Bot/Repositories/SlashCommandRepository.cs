using XudonaxBot.Bot.Repositories.Interfaces;
using XudonaxBot.Commands.Interfaces;

namespace XudonaxBot.Bot.Repositories
{
    internal class SlashCommandRepository : IReadOnlyRepository<ISlashCommand>
    {
        private readonly Dictionary<string, ISlashCommand> _commandHandlers;

        public SlashCommandRepository(IEnumerable<ISlashCommand> commands)
        {
            _commandHandlers = new Dictionary<string, ISlashCommand>();

            foreach (var command in commands)
            {
                _commandHandlers.Add(command.Name, command);
            }
        }

        public ISlashCommand Get(string name) => _commandHandlers[name];

        public IEnumerable<ISlashCommand> GetAll() => _commandHandlers.Values;
    }
}
