using System.Linq.Expressions;

namespace XudonaxBot.DAL.Repositories.Interfaces
{
    public interface IReadOnlyRepository<T, Tid>
    {
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        T? Get(Tid id);
        IEnumerable<T> GetAll();
    }
}
