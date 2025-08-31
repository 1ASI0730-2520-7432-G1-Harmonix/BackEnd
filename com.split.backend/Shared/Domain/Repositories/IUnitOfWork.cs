﻿namespace com.split.backend.Shared.Domain.Repositories;

public interface IUnitOfWork
{
    /// <summary>
    /// Saves changes to the repository
    /// </summary>
    /// <returns></returns>
    Task CompleteAsync();
}