# TodoList - Simple C# Example Project

A simple TodoList application for testing the Enterprise Agentic Test Workflow System.

## Project Structure

```
src/TodoList/
  ├── TodoItem.cs           - Todo item entity with properties and methods
  └── TodoListManager.cs    - Manager class with CRUD operations

Tests/
  └── Tests.csproj          - xUnit test project (no tests yet)
```

## Features

### TodoItem
- Properties: Id, Title, Description, IsCompleted, CreatedAt
- Methods: Complete(), Uncomplete()

### TodoListManager
- AddItem(title, description) - Add new todo item
- RemoveItem(id) - Remove item by ID
- GetItem(id) - Get single item
- GetAllItems() - Get all items
- GetCompletedItems() - Get completed items only
- GetPendingItems() - Get pending items only
- CompleteItem(id) - Mark item as completed
- GetTotalCount() - Get total item count
- Clear() - Clear all items

## Building

```bash
dotnet build src/TodoList/TodoList.csproj
dotnet build Tests/Tests.csproj
```

## Testing

Currently no tests exist. This project is ready for the agentic workflow to generate comprehensive test coverage.

## Purpose

This simple project serves as a clean example for testing the Enterprise Agentic Test Workflow System with:
- Clear, simple business logic
- Multiple methods to test (happy path, edge cases, exceptions)
- No existing tests (perfect for demonstration)
