using System.Threading;
using System.Threading.Tasks;

namespace UnitOfWorkSample.Interface
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
