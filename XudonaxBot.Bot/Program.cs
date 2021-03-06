using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using XudonaxBot.Bot.Extensions;
using XudonaxBot.Bot.Models.Options;
using XudonaxBot.Bot.Repositories;
using XudonaxBot.Bot.Services;
using XudonaxBot.Bot.Services.Interfaces;
using XudonaxBot.Commands.Implementations;
using XudonaxBot.Commands.Interfaces;
using XudonaxBot.DAL;
using XudonaxBot.DAL.Repositories.Interfaces;
using XudonaxBot.External.Tenor;

namespace XudonaxBot.Bot
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            return host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigureAppConfiguration)
                .ConfigureLogging(ConfigureLogging)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureAppConfiguration(HostBuilderContext context, IConfigurationBuilder configuration)
        {
            configuration.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {
            services
                .AddLogging()
                .AddOptions();

            services.Configure<BotOptions>(context.Configuration.GetRequiredSection("BotOptions"));

            //services
            //    .AddTransient<ISlashCommand, PingCommand>()
            //    .AddTransient<ISlashCommand, PongCommand>()
            //    .AddTransient<ISlashCommand, CatgirlCommand>()
            //    .AddTransient<ISlashCommand, HeadpatCommand>()
            //    .AddTransient<ISlashCommand, HugCommand>();

            services.RegisterAllImplementationsOf<ISlashCommand>(typeof(ISlashCommand).Assembly);

            services
                .AddSingleton<ILoggingService, LoggingService>()
                .AddSingleton<IReadOnlyRepository<ISlashCommand, string>, SlashCommandRepository>()
                .AddSingleton<DiscordSocketClient>();

            services
                .AddTransient<ISlashCommandRegistrationService, SlashCommandRegistrationService>()
                .AddTransient<ISlashCommandHandlingService, SlashCommandHandlingService>()
                .AddTransient<ITenorService, TenorService>();

            services
                .AddHttpClient<ITenorService, TenorService>();

            services.AddDbContext<BotContext>(options => 
                options.UseSqlite(
                    context.Configuration.GetConnectionString("BotContext"),
                    b => b.MigrationsAssembly(typeof(BotContext).Assembly.FullName)));

            // Add hosted service(s), should be the last line(s)
            services.AddHostedService<BotHostedService>();
        }

        private static void ConfigureLogging(HostBuilderContext context, ILoggingBuilder logging)
        {

        }
    }
}
