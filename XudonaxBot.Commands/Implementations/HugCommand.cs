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
        private const string CatgirlQuery = "anime hug";

        private readonly ITenorService _service;

        public HugCommand(ITenorService service)
        {
            _service = service;
        }

        public string Name => "hug";

        public string Description => "Get yourself a nice warm hug";

        public async Task HandleAsync(SocketSlashCommand command)
        {
            var gif = await _service.GetRandomGifFor(CatgirlQuery);
            
            if (gif != null)
            {
                var gifEmbed = new EmbedBuilder
                {
                    ImageUrl = gif,
                    Title = $"Here's your hug {command.User.Username}"
                }.Build();

                await command.RespondAsync(
                text: $"Have a hug {command.User.Mention}",
                allowedMentions: new AllowedMentions(AllowedMentionTypes.Users),
                embed: gifEmbed);
            }
            else
            {
                await command.RespondAsync($"Sorry, no hugs available right now {command.User.Mention} 😢");
            }
        }
    }
}
