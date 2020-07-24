using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BartenderSupportSystem.Shared.Utils;

namespace BartenderSupportSystem.Client.Repositories
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll(bool includeAllPrefix = false);
        Task<PaginatedResponse<List<T>>> GetPaginated(PaginationDto paginationDto);
        Task<T> GetOne(Guid id);
        Task AddOne(T item);
        Task UpdateOne(Guid id, T item);
        Task DeleteOne(Guid id);
    }
}
