using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Data.Contracts
{
    public interface IRepository<TEntity>
        where TEntity : class, IDbEntity
    {
        Task<TEntity> AddAsync(TEntity item);

        ICollection<TEntity> AddRange(ICollection<TEntity> items);

        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> items);

        Task<TEntity> GetAsync(string id);

        Task<bool> ExistsAsync(string id);
    }
}