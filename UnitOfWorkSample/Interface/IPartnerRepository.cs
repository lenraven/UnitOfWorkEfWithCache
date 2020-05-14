using System.Collections.Generic;
using System.Threading.Tasks;

using UnitOfWorkSample.Model;

namespace UnitOfWorkSample.Interface
{
    public interface IPartnerRepository
    {
        void AddPartner(Partner partner);
        Task<IEnumerable<Partner>> GetPartnersAsync();
        Task<Partner> GetPartnerAsync(int id);
        void DeletePartner(Partner partner);
    }
}
