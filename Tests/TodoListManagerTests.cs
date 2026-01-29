using System;
using System.Collections.Generic;
using Xunit;
using TodoList;

public class TodoListManagerTests
{
    private TodoListManager CreateManagerWithSampleData()
    {
        var manager = new TodoListManager();
        manager.AddItem("Task 1", "Description 1", Priority.Medium);
        manager.AddItem("Task 2", "Description 2", Priority.High);
        return manager;
    }

    [Fact]
    public void AddItem_ValidParameters_AddsItemSuccessfully()
    {
        // Arrange
        var manager = new TodoListManager();

        // Act
        var item = manager.AddItem("Test Task", "Test Description", Priority.Low);

        // Assert
        Assert.NotNull(item);
        Assert.Equal("Test Task", item.Title);
    }

    [Fact]
    public void RemoveItem_ExistingItem_RemovesItemSuccessfully()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();
        var item = manager.AddItem("Task to Remove", "Description", Priority.None);

        // Act
        var result = manager.RemoveItem(item.Id);

        // Assert
        Assert.True(result);
        Assert.Null(manager.GetItem(item.Id));
    }

    [Fact]
    public void GetItem_ExistingItem_ReturnsItem()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();
        var expectedItem = manager.AddItem("Expected Task", "Expected Description", Priority.Medium);

        // Act
        var item = manager.GetItem(expectedItem.Id);

        // Assert
        Assert.Equal(expectedItem, item);
    }

    [Fact]
    public void CompleteItem_ExistingItem_CompletesItemSuccessfully()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();

        // Act
        var result = manager.CompleteItem(1); // Assuming ID 1 exists

        // Assert
        Assert.True(result);
        Assert.True(manager.GetItem(1)?.IsCompleted);
    }

    [Fact]
    public void GetAllItems_AddMultipleItems_ReturnsAllItems()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();

        // Act
        var items = manager.GetAllItems();

        // Assert
        Assert.Equal(2, items.Count); // Assuming initial sample data has 2 items
    }
}