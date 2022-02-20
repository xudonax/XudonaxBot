using Discord;
using Discord.WebSocket;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.External.Tenor;

namespace XudonaxBot.Commands.Implementations
{
    public class HeadpatCommand : ISlashCommand
    {
        private const string SearchQuery = "anime headpat";

        private readonly ITenorService _service;

        public HeadpatCommand(ITenorService service)
        {
            _service = service;
        }

        public string Name => "headpat";

        public string Description => "Get a free headpat!";

        public async Task HandleAsync(SocketSlashCommand command)
        {
            var gif = await _service.GetRandomGifFor(SearchQuery);

            if (gif != null)
            {
                var gifEmbed = new EmbedBuilder
                {
                    ImageUrl = gif,
                    Title = $"Here's your headpat {command.User.Username}"
                }.Build();

                await command.RespondAsync(
                    text: $"Have a headpat {command.User.Mention}",
                    allowedMentions: new AllowedMentions(AllowedMentionTypes.Users),
                    embed: gifEmbed);
            } 
            else
            {
                await command.RespondAsync($"Sorry, no headpats available at this moment {command.User.Mention} 😢", ephemeral: true);
            }
        }
    }
}
