namespace Luftborn.Server.Core.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}
