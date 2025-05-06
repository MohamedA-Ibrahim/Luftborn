using Luftborn.Server.Core.Domain.Interfaces;
using Luftborn.Server.Infrastructure.Persistence.Repositories;

namespace Luftborn.Server.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ITodoRepository _todoRepository;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ITodoRepository TodoRepository => _todoRepository ??= new TodoRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
