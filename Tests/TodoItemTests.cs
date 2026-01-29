using System;
using Xunit;
using TodoList;

public class TodoItemTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesItem()
    {
        // Arrange
        var id = 1;
        var title = "Test Task";
        var description = "Test Description";
        var priority = Priority.Medium;

        // Act
        var item = new TodoItem(id, title, description, priority);

        // Assert
        Assert.NotNull(item);
        Assert.Equal(id, item.Id);
        Assert.Equal(title, item.Title);
        Assert.Equal(description, item.Description);
        Assert.Equal(priority, item.Priority);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_InvalidTitle_ThrowsArgumentException(string invalidTitle)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TodoItem(1, invalidTitle));
    }

    [Fact]
    public void Complete_UncompletedItem_SetsIsCompletedToTrue()
    {
        // Arrange
        var item = new TodoItem(1, "Test Task");

        // Act
        item.Complete();

        // Assert
        Assert.True(item.IsCompleted);
        Assert.NotNull(item.CompletedAt);
    }

    [Fact]
    public void Uncomplete_CompletedItem_SetsIsCompletedToFalse()
    {
        // Arrange
        var item = new TodoItem(1, "Test Task");
        item.Complete();

        // Act
        item.Uncomplete();

        // Assert
        Assert.False(item.IsCompleted);
        Assert.Null(item.CompletedAt);
    }

    [Fact]
    public void SetDueDate_ValidDate_SetsDueDate()
    {
        // Arrange
        var item = new TodoItem(1, "Test Task");
        var futureDate = DateTime.UtcNow.AddDays(1);

        // Act
        item.SetDueDate(futureDate);

        // Assert
        Assert.Equal(futureDate, item.DueDate);
    }

    [Fact]
    public void SetDueDate_PastDate_ThrowsArgumentException()
    {
        // Arrange
        var item = new TodoItem(1, "Test Task");
        var pastDate = DateTime.UtcNow.AddDays(-1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => item.SetDueDate(pastDate));
    }

    [Fact]
    public void UpdateTitle_ValidTitle_UpdatesTitle()
    {
        // Arrange
        var item = new TodoItem(1, "Original Title");
        var newTitle = "Updated Title";

        // Act
        item.UpdateTitle(newTitle);

        // Assert
        Assert.Equal(newTitle, item.Title);
    }

    [Fact]
    public void UpdateTitle_InvalidTitle_ThrowsArgumentException()
    {
        // Arrange
        var item = new TodoItem(1, "Original Title");
        var invalidTitle = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => item.UpdateTitle(invalidTitle));
    }
}