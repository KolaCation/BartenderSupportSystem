using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Client.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetOne(Guid id);
        Task AddOne(T item);
        Task UpdateOne(Guid id, T item);
        Task DeleteOne(Guid id);
    }
}
