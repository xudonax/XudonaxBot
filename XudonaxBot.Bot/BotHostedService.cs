using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XudonaxBot.Bot.Models.Options;
using XudonaxBot.Bot.Services.Interfaces;

namespace XudonaxBot.Bot
{
    public class BotHostedService : IHostedService
    {
        private readonly DiscordSocketClient _client;
        private readonly BotOptions _botOptions;
        private readonly ILoggingService _logging;
        private readonly ISlashCommandRegistrationService _registrationService;
        private readonly ISlashCommandHandlingService _commandHandlingService;
        private readonly ILogger _logger;

        public BotHostedService(
            IOptions<BotOptions> options,
            DiscordSocketClient client,
            ILoggingService logging,
            ISlashCommandRegistrationService registrationService,
            ISlashCommandHandlingService commandHandlingService,
            ILogger<BotHostedService> logger)
        {
            _client = client;
            _botOptions = options.Value;
            _logging = logging;
            _registrationService = registrationService;
            _commandHandlingService = commandHandlingService;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_botOptions.Token))
                throw new ArgumentNullException(nameof(_botOptions.Token), "No token found, exiting!");

            EventRegistration();

            await _client.LoginAsync(TokenType.Bot, _botOptions.Token);
            await _client.StartAsync();
        }

        private async Task JoinedGuild(SocketGuild arg)
        {
            _logger.LogDebug("Joined server \"{Name}\", ID: {ID}, Icon: {Icon}", arg.Name, arg.Id, arg.IconUrl);
            await _registrationService.RegisterAllCommands(arg.Id);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.StopAsync();
            await _client.LogoutAsync();
        }

        private void EventRegistration()
        {
            _client.Log += _logging.LogAsync;
            _client.SlashCommandExecuted += _commandHandlingService.Handle;

            // TODO: Move these to somewhere else
            _client.Ready += Ready;
            _client.JoinedGuild += JoinedGuild;
            _client.GuildScheduledEventStarted += GuildScheduledEventStarted;
        }

        private async Task Ready()
        {
            await _registrationService.UpdateCommands();
        }

        private Task GuildScheduledEventStarted(SocketGuildEvent arg)
        {
            return Task.CompletedTask;
        }
    }
}
