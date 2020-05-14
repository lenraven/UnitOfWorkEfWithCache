namespace UnitOfWorkSample.Interface
{
    public interface ICacheUnitOfWork
    {
        void Attach<TEntity>(TEntity entity) where TEntity : class;
    }
}