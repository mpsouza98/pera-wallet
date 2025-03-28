﻿namespace PeraInvest.Domain.SeedWork {
    public interface IUnitOfWork {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
