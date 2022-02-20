using Discord;

namespace XudonaxBot.Commands.Interfaces
{
    public interface IHasSubOptions
    {
        public IEnumerable<SlashCommandOptionBuilder> SubOptions { get; }
    }
}
