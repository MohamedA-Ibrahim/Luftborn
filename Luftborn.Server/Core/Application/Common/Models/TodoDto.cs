namespace Luftborn.Server.Core.Application.Common.Models;

public class TodoDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public DateTime Created { get; set; }
    public DateTime? LastModified { get; set; }
} 