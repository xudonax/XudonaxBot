using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.External.Tenor;

namespace XudonaxBot.Commands.Implementations
{
    public class HugCommand : ISlashCommand
    {
        private const string SearchQuery = "anime hug";

        private readonly ITenorService _service;

        public HugCommand(ITenorService service)
        {
            _service = service;
        }

        public string Name => "hug";

        public string Description => "Get yourself a nice warm hug";

        public IEnumerable<SlashCommandOptionBuilder> SubOptions
        {
            get
            {
                yield return new SlashCommandOptionBuilder()
                    .WithName("user")
                    .WithDescription("Who do you want to hug?")
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.User);
            }
        }

        public async Task HandleAsync(SocketSlashCommand command)
        {
            var gif = await _service.GetRandomGifFor(SearchQuery);
            
            if (gif != null)
            {
                var user = command.User;

                if (command.Data.Options.Count > 0)
                {
                    var userCommand = command.Data.Options.Where(x => x.Name == "user").FirstOrDefault();
                    if (userCommand != null && userCommand.Value is SocketGuildUser socketGuildUser)
                        user = socketGuildUser;
                }

                var gifEmbed = new EmbedBuilder
                {
                    ImageUrl = gif,
                    Title = $"Here's your hug {user.Username}"
                }.Build();

                await command.RespondAsync(
                text: $"Have a hug {user.Mention}",
                allowedMentions: new AllowedMentions(AllowedMentionTypes.Users),
                embed: gifEmbed);
            }
            else
            {
                await command.RespondAsync($"Sorry, no hugs available right now {command.User.Mention} 😢", ephemeral: true);
            }
        }
    }
}
