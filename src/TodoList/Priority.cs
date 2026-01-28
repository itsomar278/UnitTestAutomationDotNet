namespace TodoList;

/// <summary>
/// Represents the priority level of a todo item.
/// </summary>
public enum Priority
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Critical = 4
}

/// <summary>
/// Extension methods for the Priority enum.
/// </summary>
public static class PriorityExtensions
{
    public static string GetDisplayName(this Priority priority)
    {
        return priority switch
        {
            Priority.None => "No Priority",
            Priority.Low => "Low",
            Priority.Medium => "Medium",
            Priority.High => "High",
            Priority.Critical => "Critical",
            _ => "Unknown"
        };
    }

    public static bool IsUrgent(this Priority priority)
    {
        return priority >= Priority.High;
    }

    public static int GetSortOrder(this Priority priority)
    {
        return (int)Priority.Critical - (int)priority;
    }
}
