using Anonymized.Assessment.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Anonymized.Assessment.Data.Repositories
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IDbEntity
    {
        protected AccountManagementContext Context { get; }
        protected DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public EntityFrameworkRepository(AccountManagementContext context)
        {
            Context = context;
        }

        public async Task<bool> ExistsAsync(string id) =>
            await DbSet.FindAsync(id) != null;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);

            return await Context.SaveChangesAsync() > 0 ? entity : null;
        }

        public ICollection<TEntity> AddRange(ICollection<TEntity> entities)
        {
            DbSet.AddRange(entities);

            return Context.SaveChanges() > 0 ? entities : null;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            await DbSet.AddRangeAsync(entities);

            return await Context.SaveChangesAsync() > 0 ? entities : null;
        }

        public Task<TEntity> GetAsync(string id) =>
            DbSet.FindAsync(id);
    }
}