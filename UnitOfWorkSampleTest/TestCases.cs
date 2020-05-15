using System.Collections;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using SimpleInjector;

using UnitOfWorkSample.Implementation;
using UnitOfWorkSample.Implementation.Cache;
using UnitOfWorkSample.Implementation.Cache.InMemory;
using UnitOfWorkSample.Implementation.Sql;
using UnitOfWorkSample.Implementation.Sql.Context;
using UnitOfWorkSample.Interface;
using UnitOfWorkSample.Interface.Cache;
using UnitOfWorkSample.Model;

namespace UnitOfWorkSampleTest
{
    public class TestCases : IEnumerable<TestCase>
    {
        private readonly IEnumerable<TestCase> _testCases = new List<TestCase>()
        {
            new TestCase("SqlTestCase", c =>
            {
                c.Register(() =>
                {
                    var options = new DbContextOptionsBuilder<DemoDbContext>()
                        .UseLoggerFactory(Logger.EfLoggerFactory).Options;
                    return new DemoDbContext(options);
                }, Lifestyle.Scoped);

                c.Register<IPartnerRepository, PartnerSqlRepository>();
                c.Register<IOrganizationRepository, OrganizationSqlRepository>();
                c.Register<ILocaleRepository, LocaleSqlRepository>();
                
                c.Register<IUnitOfWork>(c.GetInstance<DemoDbContext>);
            }),

            new TestCase("CacheTestCase", c =>
            {
                c.Register<ICache<IEnumerable<Locale>>, InMemoryCache<IEnumerable<Locale>>>(Lifestyle.Singleton);

                c.Register(() =>
                {
                    var options = new DbContextOptionsBuilder<DemoDbContext>()
                        .UseLoggerFactory(Logger.EfLoggerFactory).Options;
                    return new DemoDbContext(options);
                }, Lifestyle.Scoped);

                c.Register<IPartnerRepository, PartnerSqlRepository>();
                c.Register<IOrganizationRepository, OrganizationSqlRepository>();
                c.Register<ILocaleRepository, LocaleCacheRepository>();
                c.Register<LocaleSqlRepository>();
                
                c.Register<IUnitOfWork>(c.GetInstance<DemoDbContext>);
                c.Register<ICacheUnitOfWork>(c.GetInstance<DemoDbContext>);
            })
        };

        public IEnumerator<TestCase> GetEnumerator()
        {
            return _testCases.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}