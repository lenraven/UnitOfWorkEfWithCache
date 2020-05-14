using System;

using UnitOfWorkSample.Implementation.Sql.Context;
using UnitOfWorkSample.Interface;

namespace UnitOfWorkSample.Implementation
{
    public class CacheUnitOfWork : ICacheUnitOfWork
    {
        private readonly DemoDbContext _context;

        public CacheUnitOfWork(DemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Attach(entity);
        }
    }
}
