using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Domain.CarteiraAggregate.Repository
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
