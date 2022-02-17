namespace XudonaxBot.Bot.Repositories.Interfaces
{
    internal interface IReadOnlyRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(string name);
    }
}
