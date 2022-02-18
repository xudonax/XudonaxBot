using XudonaxBot.DAL;
using XudonaxBot.DAL.Entities;

namespace XudonaxBot.DAL.Repositories
{
    internal class ServerRepository : GenericRepository<Server>
    {
        public ServerRepository(BotContext botContext) : base(botContext) { }

    }
}
