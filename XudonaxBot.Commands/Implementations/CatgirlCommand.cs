using Discord;
using Discord.WebSocket;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.External.Tenor;

namespace XudonaxBot.Commands.Implementations
{
    public class CatgirlCommand : ISlashCommand
    {
        private const string CatgirlQuery = "anime catgirl";

        private readonly ITenorService _service;

        public CatgirlCommand(ITenorService service)
        {
            _service = service;
        }

        public string Name => "catgirl";

        public string Description => "Fetch a random catgirl";

        public async Task HandleAsync(SocketSlashCommand command)
        {
            var gif = await _service.GetRandomGifFor(CatgirlQuery);
            
            if (gif != null)
            {
                var gifEmbed = new EmbedBuilder
                {
                    ImageUrl = gif,
                    Title = $"Here's your catgirl {command.User.Username}"
                }.Build();

                await command.RespondAsync(
                    text: $"Have a catgirl {command.User.Mention}",
                    allowedMentions: new AllowedMentions(AllowedMentionTypes.Users),
                    embed: gifEmbed);
            }
            else
            {
                await command.RespondAsync($"Sorry, someone let the catgirls out {command.User.Mention} 😢");
            }
        }
    }
}
