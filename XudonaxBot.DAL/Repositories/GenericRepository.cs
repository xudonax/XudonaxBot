using System.Linq.Expressions;
using XudonaxBot.DAL.Repositories.Interfaces;

namespace XudonaxBot.DAL.Repositories
{
    internal abstract class GenericRepository<T> : IRepository<T>, IAsyncRepository<T> where T : class, new()
    {
        protected readonly BotContext _context;

        public GenericRepository(BotContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public T? Get(Guid id)
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public void UpdateRange(T[] entities)
        {
            _context.Set<T>().UpdateRange(entities);
        }
    }
}
