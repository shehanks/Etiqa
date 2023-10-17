using Etiqa.Domain.DataModels;
using Etiqa.Domain.RequestModels;

namespace Etiqa.Services.Contract
{
    public interface IServiceBase<TEntity, TEntityLoadOptions>
        where TEntity : EntityBase
        where TEntityLoadOptions : EntityListLoadOptions
    {
        string MakeCacheKey(object id);

        string MakeCacheKey(TEntity entity);

        string MakeListCacheKey(TEntityLoadOptions loadOptions);
    }
}
