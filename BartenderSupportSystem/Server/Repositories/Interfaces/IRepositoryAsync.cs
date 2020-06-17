using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BartenderSupportSystem.Server.Repositories.Interfaces
{
    public interface IRepositoryAsync<T>
    {
        Task<IReadOnlyCollection<T>> GetAll();
        Task<T> GetOne(Guid id);
        Task<T> AddOne(T item);
        Task<T> UpdateOne(T item);
        Task DeleteOne(Guid id);
        Task<bool> Exists(Guid id);
        Task Detach(Guid id);
    }
}
