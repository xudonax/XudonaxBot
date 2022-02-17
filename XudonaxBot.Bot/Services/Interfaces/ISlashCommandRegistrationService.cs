namespace XudonaxBot.Bot.Services.Interfaces
{
    public interface ISlashCommandRegistrationService
    {
        Task RegisterAllCommands(ulong guildId);
        Task UpdateCommands();
        Task UpdateCommandsInGuild(ulong guildId);
    }
}
