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
    public class OrganizationSqlRepository : IOrganizationRepository
    {
        private readonly DemoDbContext _context;

        public OrganizationSqlRepository(DemoDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddOrganization(Organization organization)
        {
            _context.Organizations.Add(organization);
        }

        public async Task<IEnumerable<Organization>> GetOrganizationsAsync()
        {
            return await _context.Organizations.ToListAsync();
        }

        public Task<Organization> GetOrganizationAsync(int id)
        {
            return _context.Organizations.FindAsync(id).AsTask();
        }

        public void DeleteOrganization(Organization organization)
        {
            _context.Organizations.Remove(organization);
        }
    }
}
