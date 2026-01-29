using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using TodoList;

public class TodoStatisticsTests
{
    private TodoListManager CreateManagerWithSampleData()
    {
        var manager = new TodoListManager();
        var item1 = manager.AddItem("Task 1", "Description 1", Priority.Medium);
        item1.Complete();
        manager.AddItem("Task 2", "Description 2", Priority.High);
        return manager;
    }

    [Fact]
    public void TotalItems_WithSampleData_ReturnsCorrectCount()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();
        var statistics = new TodoStatistics(manager);

        // Act
        var totalItems = statistics.TotalItems;

        // Assert
        Assert.Equal(2, totalItems);
    }

    [Fact]
    public void CompletedItems_WithSampleData_ReturnsCorrectCount()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();
        var statistics = new TodoStatistics(manager);

        // Act
        var completedItems = statistics.CompletedItems;

        // Assert
        Assert.Equal(1, completedItems);
    }

    [Fact]
    public void PendingItems_WithSampleData_ReturnsCorrectCount()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();
        var statistics = new TodoStatistics(manager);

        // Act
        var pendingItems = statistics.PendingItems;

        // Assert
        Assert.Equal(1, pendingItems);
    }

    [Fact]
    public void CompletionRate_WithSampleData_CalculatesCorrectly()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();
        var statistics = new TodoStatistics(manager);

        // Act
        var completionRate = statistics.CompletionRate;

        // Assert
        Assert.Equal(50, completionRate);
    }

    [Fact]
    public void GetPriorityBreakdown_WithSampleData_ReturnsCorrectBreakdown()
    {
        // Arrange
        var manager = CreateManagerWithSampleData();
        var statistics = new TodoStatistics(manager);

        // Act
        var breakdown = statistics.GetPriorityBreakdown();

        // Assert
        Assert.Equal(1, breakdown[Priority.Medium]);
        Assert.Equal(1, breakdown[Priority.High]);
        Assert.Equal(0, breakdown[Priority.None]); // Assuming no items with Priority.None
    }
}