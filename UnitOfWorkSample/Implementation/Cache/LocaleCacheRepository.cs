using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using UnitOfWorkSample.Implementation.Sql;
using UnitOfWorkSample.Interface;
using UnitOfWorkSample.Interface.Cache;
using UnitOfWorkSample.Model;

namespace UnitOfWorkSample.Implementation.Cache
{
    public class LocaleCacheRepository : ILocaleRepository
    {
        private static readonly string CacheKey = "Locales";

        private readonly LocaleSqlRepository _dataRepository;
        private readonly ICache<IEnumerable<Locale>> _localeCache;
        private readonly ICacheUnitOfWork _cacheUnitOfWork;

        public LocaleCacheRepository(LocaleSqlRepository dataRepository, ICache<IEnumerable<Locale>> localeCache, ICacheUnitOfWork cacheUnitOfWork)
        {
            _dataRepository = dataRepository ?? throw new ArgumentNullException(nameof(dataRepository));
            _localeCache = localeCache ?? throw new ArgumentNullException(nameof(localeCache));
            _cacheUnitOfWork = cacheUnitOfWork ?? throw new ArgumentNullException(nameof(cacheUnitOfWork));
        }

        public async Task<IEnumerable<Locale>> GetLocalesAsync()
        {
            var result = await GetLocalesInternalAsync();
            foreach (var item in result)
            {
                _cacheUnitOfWork.Attach(item);
            }
            return result;
        }

        public async Task<Locale> GetLocaleAsync(string id)
        {
            var locales = await GetLocalesInternalAsync();
            var result = locales.Single(p => p.Id == id);
            _cacheUnitOfWork.Attach(result);
            return result;
        }

        public Task<IEnumerable<Locale>> GetLocalesInternalAsync()
        {
            return _localeCache.GetOrSetAsync(CacheKey, _dataRepository.GetLocalesAsync);
        }

        
    }
}
