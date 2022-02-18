namespace XudonaxBot.DAL.Repositories.Interfaces
{
    public interface IRepository<T> : IReadOnlyRepository<T, Guid> where T : class, new()
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(T[] entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
