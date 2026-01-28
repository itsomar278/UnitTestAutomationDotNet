namespace TodoList;

/// <summary>
/// Represents a single todo item with priority, category, and due date support.
/// </summary>
public class TodoItem
{
    public int Id { get; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; }
    public Priority Priority { get; set; }
    public DateTime? DueDate { get; private set; }
    public TodoCategory? Category { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    public TodoItem(int id, string title, string description = "", Priority priority = Priority.None)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        Id = id;
        Title = title;
        Description = description;
        Priority = priority;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            CompletedAt = DateTime.UtcNow;
        }
    }

    public void Uncomplete()
    {
        if (IsCompleted)
        {
            IsCompleted = false;
            CompletedAt = null;
        }
    }

    public void UpdateTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException("Title cannot be empty", nameof(newTitle));
        Title = newTitle;
    }

    public void SetDueDate(DateTime? dueDate)
    {
        if (dueDate.HasValue && dueDate.Value < DateTime.UtcNow.Date)
            throw new ArgumentException("Due date cannot be in the past", nameof(dueDate));
        DueDate = dueDate;
    }

    public void ClearDueDate() => DueDate = null;

    public void SetCategory(TodoCategory? category) => Category = category;

    public bool IsOverdue() => !IsCompleted && DueDate.HasValue && DueDate.Value < DateTime.UtcNow;

    public bool IsDueToday() => !IsCompleted && DueDate.HasValue && DueDate.Value.Date == DateTime.UtcNow.Date;

    public int? GetDaysUntilDue() => DueDate.HasValue ? (DueDate.Value.Date - DateTime.UtcNow.Date).Days : null;

    public override string ToString()
    {
        var status = IsCompleted ? "[X]" : "[ ]";
        var priority = Priority != Priority.None ? $" [{Priority}]" : "";
        var due = DueDate.HasValue ? $" (Due: {DueDate.Value:yyyy-MM-dd})" : "";
        return $"{status} {Title}{priority}{due}";
    }
}
