using System.Linq.Expressions;

namespace Etiqa.Repository.Contract
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> InsertAsync(TEntity entity);

        Task InsertRangeAsync(IEnumerable<TEntity> entityList);

        Task DeleteAsync(object id);

        Task DeleteAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task<TEntity?> GetByIdAsync(object id);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            int? take = null,
            int? skip = null,
            string includeProperties = "");
    }
}