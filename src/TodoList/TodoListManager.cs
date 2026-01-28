namespace TodoList;

public class TodoListManager
{
    private readonly List<TodoItem> _items;
    private int _nextId;

    public TodoListManager()
    {
        _items = new List<TodoItem>();
        _nextId = 1;
    }

    public TodoItem AddItem(string title, string description = "")
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title cannot be empty", nameof(title));
        }

        var item = new TodoItem(_nextId++, title, description);
        _items.Add(item);
        return item;
    }

    public bool RemoveItem(int id)
    {
        var item = _items.FirstOrDefault(x => x.Id == id);
        if (item == null)
        {
            return false;
        }

        _items.Remove(item);
        return true;
    }

    public TodoItem? GetItem(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public List<TodoItem> GetAllItems()
    {
        return new List<TodoItem>(_items);
    }

    public List<TodoItem> GetCompletedItems()
    {
        return _items.Where(x => x.IsCompleted).ToList();
    }

    public List<TodoItem> GetPendingItems()
    {
        return _items.Where(x => !x.IsCompleted).ToList();
    }

    public bool CompleteItem(int id)
    {
        var item = GetItem(id);
        if (item == null)
        {
            return false;
        }

        item.Complete();
        return true;
    }

    public int GetTotalCount()
    {
        return _items.Count;
    }

    public void Clear()
    {
        _items.Clear();
        _nextId = 1;
    }
}
