namespace Luftborn.Server.Core.Domain.Interfaces;

public interface IUnitOfWork
{
    ITodoRepository TodoRepository { get; }
    Task<int> SaveChangesAsync();
}
