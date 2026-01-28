namespace TodoList;

/// <summary>
/// Provides statistics and analytics for a todo list.
/// </summary>
public class TodoStatistics
{
    private readonly TodoListManager _manager;

    public TodoStatistics(TodoListManager manager)
    {
        _manager = manager ?? throw new ArgumentNullException(nameof(manager));
    }

    public int TotalItems => _manager.GetTotalCount();
    public int CompletedItems => _manager.GetCompletedItems().Count;
    public int PendingItems => _manager.GetPendingItems().Count;
    public int OverdueItems => _manager.GetOverdueItems().Count;
    public int UrgentItems => _manager.GetUrgentItems().Count;

    public double CompletionRate
    {
        get
        {
            if (TotalItems == 0) return 0;
            return Math.Round((double)CompletedItems / TotalItems * 100, 2);
        }
    }

    public Dictionary<Priority, int> GetPriorityBreakdown()
    {
        var breakdown = new Dictionary<Priority, int>();
        var allItems = _manager.GetAllItems();

        foreach (Priority priority in Enum.GetValues<Priority>())
            breakdown[priority] = allItems.Count(x => x.Priority == priority);

        return breakdown;
    }

    public Dictionary<string, int> GetCategoryBreakdown()
    {
        var breakdown = new Dictionary<string, int>();
        var allItems = _manager.GetAllItems();

        var uncategorized = allItems.Count(x => x.Category == null);
        if (uncategorized > 0)
            breakdown["Uncategorized"] = uncategorized;

        foreach (var category in _manager.GetAllCategories())
        {
            var count = allItems.Count(x => x.Category?.Id == category.Id);
            if (count > 0)
                breakdown[category.Name] = count;
        }

        return breakdown;
    }

    public Dictionary<DateTime, List<TodoItem>> GetUpcomingDeadlines(int days = 7)
    {
        if (days < 0)
            throw new ArgumentException("Days must be non-negative", nameof(days));

        var result = new Dictionary<DateTime, List<TodoItem>>();
        var today = DateTime.UtcNow.Date;

        for (int i = 0; i <= days; i++)
            result[today.AddDays(i)] = new List<TodoItem>();

        var pendingItems = _manager.GetPendingItems()
            .Where(x => x.DueDate.HasValue && x.DueDate.Value.Date >= today && x.DueDate.Value.Date <= today.AddDays(days));

        foreach (var item in pendingItems)
        {
            if (item.DueDate.HasValue && result.ContainsKey(item.DueDate.Value.Date))
                result[item.DueDate.Value.Date].Add(item);
        }

        return result;
    }

    public double? GetAverageCompletionTime()
    {
        var completedItems = _manager.GetCompletedItems().Where(x => x.CompletedAt.HasValue).ToList();

        if (completedItems.Count == 0)
            return null;

        var totalHours = completedItems.Sum(x => (x.CompletedAt!.Value - x.CreatedAt).TotalHours);
        return Math.Round(totalHours / completedItems.Count, 2);
    }

    public TodoSummary GetSummary()
    {
        return new TodoSummary
        {
            TotalItems = TotalItems,
            CompletedItems = CompletedItems,
            PendingItems = PendingItems,
            OverdueItems = OverdueItems,
            UrgentItems = UrgentItems,
            DueToday = _manager.GetItemsDueToday().Count,
            CompletionRate = CompletionRate,
            CategoriesCount = _manager.GetAllCategories().Count
        };
    }
}

/// <summary>
/// Summary of the todo list status.
/// </summary>
public class TodoSummary
{
    public int TotalItems { get; set; }
    public int CompletedItems { get; set; }
    public int PendingItems { get; set; }
    public int OverdueItems { get; set; }
    public int UrgentItems { get; set; }
    public int DueToday { get; set; }
    public double CompletionRate { get; set; }
    public int CategoriesCount { get; set; }

    public override string ToString() =>
        $"Total: {TotalItems}, Completed: {CompletedItems} ({CompletionRate}%), Pending: {PendingItems}, Overdue: {OverdueItems}";
}
