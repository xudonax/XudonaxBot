using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.DiceParser;

namespace XudonaxBot.Commands.Implementations
{
    public class RollCommand : ISlashCommand, IHasSubOptions
    {
        private readonly ILogger _logger;

        public RollCommand(ILogger<RollCommand> logger)
        {
            _logger = logger;
        }

        public string Name => "roll";

        public string Description => "Roll some dice";

        public IEnumerable<SlashCommandOptionBuilder> SubOptions
        {
            get
            {
                yield return new SlashCommandOptionBuilder()
                    .WithName("rpg-dice")
                    .WithDescription("What to roll, e.g. 3d6+5")
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.String);
            }
        }

        public async Task HandleAsync(SocketSlashCommand command)
        {
            const string ErrorMessage = "Don't know how to roll \"{0}\"";
            var desc = command.Data.Options.First(d => d.Name == "rpg-dice").Value as string;

            try
            {    
                if (desc == null)
                {
                    await command.RespondAsync(string.Format(ErrorMessage, desc), ephemeral: true);
                    return;
                }

                var (total, rolls) = DiceParser.DiceParser.Parse(desc).Roll();

                var rollString = string.Join(", ", rolls);

                await command.RespondAsync($"{command.User.Mention} rolled {total} ({rollString})");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "While trying to roll \"{desc}\", we caught an exception", desc);
                await command.RespondAsync(string.Format(ErrorMessage, desc), ephemeral: true);
            }
        }
    }
}
