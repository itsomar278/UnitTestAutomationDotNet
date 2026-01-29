namespace TodoList;

/// <summary>
/// Represents a category for organizing todo items.
/// </summary>
public class TodoCategory
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; private set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public DateTime CreatedAt { get; }

    public TodoCategory(string name, string description = "", string color = "#808080")
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Category name cannot be empty", nameof(name));

        Id = _nextId++;
        Name = name;
        Description = description;
        Color = ValidateColor(color) ? color : "#808080";
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException("Category name cannot be empty", nameof(newName));
        Name = newName;
    }

    public static bool ValidateColor(string color)
    {
        if (string.IsNullOrEmpty(color) || !color.StartsWith("#"))
            return false;
        if (color.Length != 7 && color.Length != 4)
            return false;
        for (int i = 1; i < color.Length; i++)
        {
            char c = color[i];
            if (!((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F') || (c >= 'a' && c <= 'f')))
                return false;
        }
        return true;
    }

    internal static void ResetIdCounter() => _nextId = 1;

    public override string ToString() => $"{Name} ({Id})";
    public override bool Equals(object? obj) => obj is TodoCategory other && Id == other.Id;
    public override int GetHashCode() => Id.GetHashCode();
}
