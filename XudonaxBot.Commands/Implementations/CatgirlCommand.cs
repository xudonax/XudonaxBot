using Discord;
using Discord.WebSocket;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.External.Tenor;

namespace XudonaxBot.Commands.Implementations
{
    public class CatgirlCommand : ISlashCommand, IHasSubOptions
    {
        private const string SearchQuery = "anime catgirl";

        private readonly ITenorService _service;

        public CatgirlCommand(ITenorService service)
        {
            _service = service;
        }

        public string Name => "catgirl";

        public string Description => "Send a random catgirl GIF";

        public IEnumerable<SlashCommandOptionBuilder> SubOptions
        {
            get
            {
                yield return new SlashCommandOptionBuilder()
                    .WithName("user")
                    .WithDescription("Which user do you want to send a catgirl?")
                    .WithRequired(false)
                    .WithType(ApplicationCommandOptionType.User);

            }
        }

        public async Task HandleAsync(SocketSlashCommand command)
        {
            var gif = await _service.GetRandomGifFor(SearchQuery);
            
            if (gif != null)
            {
                var user = command.User;

                if  (command.Data.Options.Count > 0)
                {
                    var userCommand = command.Data.Options.Where(x => x.Name == "user").FirstOrDefault();
                    if (userCommand != null && userCommand.Value is SocketGuildUser socketGuildUser)
                        user = socketGuildUser;
                }

                var gifEmbed = new EmbedBuilder
                {
                    ImageUrl = gif,
                    Title = $"Here's your catgirl {user.Username}"
                }.Build();

                await command.RespondAsync(
                    text: $"Have a catgirl {user.Mention}",
                    allowedMentions: new AllowedMentions(AllowedMentionTypes.Users),
                    embed: gifEmbed);
            }
            else
            {
                await command.RespondAsync($"Sorry, someone let the catgirls out {command.User.Mention} 😢", ephemeral: true);
            }
        }
    }
}
