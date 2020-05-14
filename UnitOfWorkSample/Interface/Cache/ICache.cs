using System;
using System.Threading.Tasks;

namespace UnitOfWorkSample.Interface.Cache
{
    public interface ICache<TEntity>
    {
        Task<TEntity> GetOrSetAsync(string cacheKey, Func<Task<TEntity>> generator);
    }
}
