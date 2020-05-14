using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using UnitOfWorkSample.Interface.Cache;

namespace UnitOfWorkSample.Implementation.Cache.InMemory
{
    public class InMemoryCache<TEntity> : ICache<TEntity>
    {
        private readonly SemaphoreSlim  semaphore = new SemaphoreSlim(1);

        private readonly Dictionary<string, TEntity> cache = new Dictionary<string, TEntity>();

        public async Task<TEntity> GetOrSetAsync(string cacheKey, Func<Task<TEntity>> generator)
        {
            if (!cache.TryGetValue(cacheKey, out var result))
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (!cache.TryGetValue(cacheKey, out result))
                    {
                        result = await generator();
                        cache.Add(cacheKey, result);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }

            return result;
        }
    }
}
