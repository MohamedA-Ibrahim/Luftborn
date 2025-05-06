using Luftborn.Server.Core.Domain.Entities;
using Luftborn.Server.Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Luftborn.Server.Infrastructure.Persistence.Repositories;

public class TodoRepository : BaseRepository<Todo>, ITodoRepository
{
    public TodoRepository(ApplicationDbContext context) : base(context)
    {
    }

} 