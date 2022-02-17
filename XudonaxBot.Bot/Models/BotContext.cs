using Microsoft.EntityFrameworkCore;
using XudonaxBot.Bot.Models.Database;

namespace XudonaxBot.Bot.Models
{
    public class BotContext : DbContext
    {
        public DbSet<Server> Servers => Set<Server>();

        public BotContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }
    }
}
