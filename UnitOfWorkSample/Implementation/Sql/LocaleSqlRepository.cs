using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using UnitOfWorkSample.Implementation.Sql.Context;
using UnitOfWorkSample.Interface;
using UnitOfWorkSample.Model;

namespace UnitOfWorkSample.Implementation.Sql
{
    public class LocaleSqlRepository : ILocaleRepository
    {
        private readonly DemoDbContext _context;

        public LocaleSqlRepository(DemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Locale>> GetLocalesAsync()
        {
            return await _context.Locales.ToListAsync();
        }

        public Task<Locale> GetLocaleAsync(string id)
        {
            return _context.Locales.FindAsync(id).AsTask();
        }
    }
}
