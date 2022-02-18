using System.Linq.Expressions;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.DAL.Repositories.Interfaces;

namespace XudonaxBot.Bot.Repositories
{
    public class SlashCommandRepository : IReadOnlyRepository<ISlashCommand, string>
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

        public IEnumerable<ISlashCommand> Find(Expression<Func<ISlashCommand, bool>> expression) => _commandHandlers.Values.Where(expression.Compile());

        public ISlashCommand Get(string name) => _commandHandlers[name];

        public IEnumerable<ISlashCommand> GetAll() => _commandHandlers.Values;
    }
}
