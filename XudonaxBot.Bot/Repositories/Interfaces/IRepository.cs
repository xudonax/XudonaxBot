using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XudonaxBot.Bot.Repositories.Interfaces
{
    internal interface IRepository<T> : IReadOnlyRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
