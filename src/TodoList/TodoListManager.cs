namespace TodoList;

/// <summary>
/// Manages a collection of todo items with filtering and sorting capabilities.
/// </summary>
public class TodoListManager
{
    private readonly List<TodoItem> _items;
    private readonly List<TodoCategory> _categories;
    private int _nextId;

    public TodoListManager()
    {
        _items = new List<TodoItem>();
        _categories = new List<TodoCategory>();
        _nextId = 1;
    }

    #region Item Management

    public TodoItem AddItem(string title, string description = "", Priority priority = Priority.None)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));

        var item = new TodoItem(_nextId++, title, description, priority);
        _items.Add(item);
        return item;
    }

    public bool RemoveItem(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null) return false;
        _items.Remove(item);
        return true;
    }

    public TodoItem? GetItem(int id) => _items.FirstOrDefault(x => x.Id == id);

    public List<TodoItem> GetAllItems() => new List<TodoItem>(_items);

    public bool CompleteItem(int id)
    {
        var item = GetItem(id);
        if (item == null) return false;
        item.Complete();
        return true;
    }

    public int GetTotalCount() => _items.Count;

    public void Clear()
    {
        _items.Clear();
        _categories.Clear();
        _nextId = 1;
        TodoCategory.ResetIdCounter();
    }

    #endregion

    #region Filtering by Status

    public List<TodoItem> GetCompletedItems() => _items.Where(x => x.IsCompleted).ToList();

    public List<TodoItem> GetPendingItems() => _items.Where(x => !x.IsCompleted).ToList();

    #endregion

    #region Filtering by Priority

    public List<TodoItem> GetItemsByPriority(Priority priority) => _items.Where(x => x.Priority == priority).ToList();

    public List<TodoItem> GetUrgentItems() => _items.Where(x => x.Priority.IsUrgent() && !x.IsCompleted).ToList();

    public List<TodoItem> GetItemsSortedByPriority() => _items.OrderBy(x => x.Priority.GetSortOrder()).ToList();

    #endregion

    #region Filtering by Due Date

    public List<TodoItem> GetOverdueItems() => _items.Where(x => x.IsOverdue()).ToList();

    public List<TodoItem> GetItemsDueToday() => _items.Where(x => x.IsDueToday()).ToList();

    public List<TodoItem> GetItemsDueWithinDays(int days)
    {
        if (days < 0)
            throw new ArgumentException("Days must be non-negative", nameof(days));

        var cutoffDate = DateTime.UtcNow.Date.AddDays(days);
        return _items.Where(x => !x.IsCompleted && x.DueDate.HasValue && x.DueDate.Value.Date <= cutoffDate).ToList();
    }

    public List<TodoItem> GetItemsSortedByDueDate() =>
        _items.OrderBy(x => x.DueDate.HasValue ? 0 : 1).ThenBy(x => x.DueDate).ToList();

    #endregion

    #region Filtering by Category

    public List<TodoItem> GetItemsByCategory(TodoCategory category) =>
        _items.Where(x => x.Category?.Id == category.Id).ToList();

    public List<TodoItem> GetUncategorizedItems() => _items.Where(x => x.Category == null).ToList();

    #endregion

    #region Category Management

    public TodoCategory AddCategory(string name, string description = "", string color = "#808080")
    {
        if (_categories.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException($"Category '{name}' already exists");

        var category = new TodoCategory(name, description, color);
        _categories.Add(category);
        return category;
    }

    public bool RemoveCategory(int categoryId)
    {
        var category = _categories.FirstOrDefault(c => c.Id == categoryId);
        if (category == null) return false;

        foreach (var item in _items.Where(i => i.Category?.Id == categoryId))
            item.SetCategory(null);

        _categories.Remove(category);
        return true;
    }

    public List<TodoCategory> GetAllCategories() => new List<TodoCategory>(_categories);

    public TodoCategory? GetCategory(int id) => _categories.FirstOrDefault(c => c.Id == id);

    public TodoCategory? GetCategoryByName(string name) =>
        _categories.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

    #endregion

    #region Search and Combined Filtering

    public List<TodoItem> SearchItems(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return new List<TodoItem>();

        var term = searchTerm.ToLowerInvariant();
        return _items.Where(x =>
            x.Title.ToLowerInvariant().Contains(term) ||
            x.Description.ToLowerInvariant().Contains(term)
        ).ToList();
    }

    public List<TodoItem> FilterItems(bool? isCompleted = null, Priority? priority = null,
        TodoCategory? category = null, bool? hasDeadline = null)
    {
        var query = _items.AsEnumerable();

        if (isCompleted.HasValue)
            query = query.Where(x => x.IsCompleted == isCompleted.Value);
        if (priority.HasValue)
            query = query.Where(x => x.Priority == priority.Value);
        if (category != null)
            query = query.Where(x => x.Category?.Id == category.Id);
        if (hasDeadline.HasValue)
            query = hasDeadline.Value ? query.Where(x => x.DueDate.HasValue) : query.Where(x => !x.DueDate.HasValue);

        return query.ToList();
    }

    #endregion
}
