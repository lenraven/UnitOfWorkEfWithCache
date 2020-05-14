using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using UnitOfWorkSample.Implementation.Sql.Context;
using UnitOfWorkSample.Interface;
using UnitOfWorkSample.Model;

namespace UnitOfWorkSample.Implementation.Sql
{
    public class PartnerSqlRepository : IPartnerRepository
    {
        private readonly DemoDbContext _context;

        public PartnerSqlRepository(DemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddPartner(Partner partner)
        {
            _context.Partners.Add(partner);
        }

        public async Task<IEnumerable<Partner>> GetPartnersAsync()
        {
            return await _context.Partners.ToListAsync();
        }

        public Task<Partner> GetPartnerAsync(int id)
        {
            return _context.Partners.FindAsync(id).AsTask();
        }

        public void DeletePartner(Partner partner)
        {
            _context.Partners.Remove(partner);
        }
    }
}
