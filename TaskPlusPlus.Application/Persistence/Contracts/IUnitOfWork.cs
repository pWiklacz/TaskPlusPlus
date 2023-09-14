﻿namespace TaskPlusPlus.Application.Persistence.Contracts;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}