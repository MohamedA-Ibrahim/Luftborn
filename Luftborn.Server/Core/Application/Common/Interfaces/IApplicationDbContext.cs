using Luftborn.Server.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Server.Core.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Todo> Todos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
