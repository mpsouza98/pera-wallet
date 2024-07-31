using PeraInvest.Domain.SeedWork;

namespace PeraInvest.Domain.CarteiraAggregate.Repository
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
