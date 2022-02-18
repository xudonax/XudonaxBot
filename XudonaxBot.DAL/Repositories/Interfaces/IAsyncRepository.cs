namespace XudonaxBot.DAL.Repositories.Interfaces
{
    public interface IAsyncRepository<T> : IReadOnlyRepository<T, Guid> where T : class, new()
    {
        public Task AddAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public Task<T?> GetAsync(Guid id);
    }
}
