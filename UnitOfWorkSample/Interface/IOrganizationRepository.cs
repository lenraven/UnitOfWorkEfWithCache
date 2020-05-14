using System.Collections.Generic;
using System.Threading.Tasks;

using UnitOfWorkSample.Model;

namespace UnitOfWorkSample.Interface
{
    public interface IOrganizationRepository
    {
        void AddOrganization(Organization organization);
        Task<IEnumerable<Organization>> GetOrganizationsAsync();
        Task<Organization> GetOrganizationAsync(int id);
        void DeleteOrganization(Organization organization);
    }
}