using Etiqa.Domain.Context;
using Etiqa.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Etiqa.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        internal EtiqaDbContext etiqaDbContext;
        internal DbSet<TEntity> dbSet;

        public RepositoryBase(EtiqaDbContext context)
        {
            etiqaDbContext = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> entityList)
        {
            if (entityList.Any())
            {
                foreach (var entity in entityList)
                    await dbSet.AddAsync(entity);
            }
        }

        public virtual Task DeleteAsync(object id)
        {
            var entity = dbSet.Find(id);
            if (entity != null)
                DeleteAsync(entity);

            return Task.CompletedTask;
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            if (etiqaDbContext.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            dbSet.Attach(entity);
            etiqaDbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public virtual async Task<TEntity?> GetByIdAsync(object id) => await dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await dbSet.AsNoTracking().ToListAsync();

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? take = null,
            int? skip = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                query = orderBy(query);
            if (skip.HasValue)
                query = query.Skip(skip.Value);
            if (take.HasValue)
                query = query.Take(take.Value);

            return await query.AsNoTracking().ToListAsync();
        }
    }
}