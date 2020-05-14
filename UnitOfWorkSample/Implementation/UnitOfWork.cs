using System;
using System.Threading.Tasks;

using UnitOfWorkSample.Implementation.Sql.Context;
using UnitOfWorkSample.Interface;

namespace UnitOfWorkSample.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DemoDbContext _context;

        public UnitOfWork(DemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
