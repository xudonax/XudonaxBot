using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using XudonaxBot.DAL.Entities;

namespace XudonaxBot.DAL
{
    public class BotContext : DbContext
    {
        public DbSet<Server> Servers => Set<Server>();

        public BotContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BotContext>
    {
        public BotContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@Directory.GetCurrentDirectory() + "/../XudonaxBot.Bot/appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BotContext>();
            var connectionString = configuration.GetConnectionString("BotContext");
            builder.UseSqlite(connectionString);
            return new BotContext(builder.Options);
        }
    }
}
