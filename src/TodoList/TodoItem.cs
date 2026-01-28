namespace TodoList;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }

    public TodoItem(int id, string title, string description = "")
    {
        Id = id;
        Title = title;
        Description = description;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        IsCompleted = true;
    }

    public void Uncomplete()
    {
        IsCompleted = false;
    }

    public void UpdateTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
        {
            throw new ArgumentException("Title cannot be empty", nameof(newTitle));
        }

        Title = newTitle;
    }
}
