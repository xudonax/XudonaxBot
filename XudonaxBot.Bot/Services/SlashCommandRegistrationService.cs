using Discord;
using Discord.Net;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using XudonaxBot.Bot.Repositories.Interfaces;
using XudonaxBot.Bot.Services.Interfaces;
using XudonaxBot.Commands.Interfaces;

namespace XudonaxBot.Bot.Services
{
    internal class SlashCommandRegistrationService : ISlashCommandRegistrationService
    {
        private readonly DiscordSocketClient _client;
        private readonly IReadOnlyRepository<ISlashCommand> _commandRepository;
        private readonly ILogger _logger;

        public SlashCommandRegistrationService(DiscordSocketClient client, IReadOnlyRepository<ISlashCommand> commandRepository, ILogger<SlashCommandRegistrationService> logger)
        {
            _client = client;
            _logger = logger;
            _commandRepository = commandRepository;
        }

        public async Task RegisterAllCommands(ulong guildId)
        {
            var guild = _client.GetGuild(guildId);

            foreach (var commandHandler in _commandRepository.GetAll())
            {
                await RegisterCommand(guild, commandHandler);
            }
        }

        public async Task UpdateCommands()
        {
            var guildIds = _client.Guilds.Select(g => g.Id);
            foreach (var guildId in guildIds) await UpdateCommandsInGuild(guildId);
        }

        public async Task UpdateCommandsInGuild(ulong guildId)
        {
            var guild = _client.GetGuild(guildId);
            var registeredCommands = await guild.GetApplicationCommandsAsync();
            var registeredCommandNames = registeredCommands.Select(c => c.Name).ToList().AsReadOnly();

            foreach (var commandHandler in _commandRepository.GetAll())
            {
                if (registeredCommandNames.Contains(commandHandler.Name)) continue;
                await RegisterCommand(guild, commandHandler);
            }
        }

        private async Task RegisterCommand(IGuild guild, ISlashCommand commandHandler)
        {
            var commandBuilder = new SlashCommandBuilder()
                    .WithName(commandHandler.Name)
                    .WithDescription(commandHandler.Description);

            var command = commandBuilder.Build();

            try
            {
                await guild.CreateApplicationCommandAsync(command);
                _logger.LogInformation("Added command \"{CommandName}\" to guild \"{GuildName}\" ({GuildId})", command.Name, guild.Name, guild.Id);
            }
            catch (HttpException exception)
            {
                _logger.LogError(exception, "Caught exception while registering command \"{CommandName}\" to guild \"{GuildName}\" ({GuildId})", command.Name, guild.Name, guild.Id);
            }
        }
    }
}
