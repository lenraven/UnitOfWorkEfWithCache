using System.Collections.Generic;
using System.Threading.Tasks;

using UnitOfWorkSample.Model;

namespace UnitOfWorkSample.Interface
{
    public interface ILocaleRepository
    {
        Task<IEnumerable<Locale>> GetLocalesAsync();
        Task<Locale> GetLocaleAsync(string id);
    }
}
