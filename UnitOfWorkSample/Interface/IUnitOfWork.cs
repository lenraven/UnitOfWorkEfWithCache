using System;
using System.Threading.Tasks;

namespace UnitOfWorkSample.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync();
    }
}
