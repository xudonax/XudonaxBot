using System.Security.Cryptography;
using Discord;
using Discord.WebSocket;
using XudonaxBot.Commands.Interfaces;

namespace XudonaxBot.Commands.Implementations
{
    public class RandomNumberCommand : ISlashCommand, IHasSubOptions
    {
        public string Name => "random";

        public string Description => "Generate a random number";

        public IEnumerable<SlashCommandOptionBuilder> SubOptions
        {
            get
            {
                yield return new SlashCommandOptionBuilder()
                    .WithName("min")
                    .WithDescription("Minimum value of the random number")
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMinValue(int.MinValue)
                    .WithMaxValue(int.MaxValue);

                yield return new SlashCommandOptionBuilder()
                    .WithName("max")
                    .WithDescription("Maximum value of the random number")
                    .WithRequired(true)
                    .WithType(ApplicationCommandOptionType.Integer)
                    .WithMinValue(int.MinValue)
                    .WithMaxValue(int.MaxValue);
            }
        }


        public async Task HandleAsync(SocketSlashCommand command)
        {
            var minValue = command.Data.Options.First(x => x.Name == "min").Value as long? ?? 0;
            var maxValue = command.Data.Options.First(x => x.Name == "max").Value as long? ?? 0;
            var randomValue = RandomNumberGenerator.GetInt32((int)minValue, (int)maxValue);

            await command.RespondAsync($"The random number is {randomValue}");
        }
    }
}
