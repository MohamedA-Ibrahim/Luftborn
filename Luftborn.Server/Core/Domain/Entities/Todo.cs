using Luftborn.Server.Core.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Luftborn.Server.Core.Domain.Entities;

public class Todo : BaseEntity
{
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
} 