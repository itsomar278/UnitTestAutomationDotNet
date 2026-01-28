using System;
using Xunit;

namespace TodoList.Tests
{
    public class TodoItemTests
    {
        [Fact]
        public void Constructor_InitializesWithGivenValues()
        {
            // Arrange
            int id = 1;
            string title = "Test Title";
            string description = "Test Description";

            // Act
            var todoItem = new TodoItem(id, title, description);

            // Assert
            Assert.Equal(id, todoItem.Id);
            Assert.Equal(title, todoItem.Title);
            Assert.Equal(description, todoItem.Description);
            Assert.False(todoItem.IsCompleted);
            Assert.True((DateTime.UtcNow - todoItem.CreatedAt).TotalSeconds < 1);
        }

        [Fact]
        public void Complete_SetsIsCompletedToTrue()
        {
            // Arrange
            var todoItem = new TodoItem(1, "Test Title");

            // Act
            todoItem.Complete();

            // Assert
            Assert.True(todoItem.IsCompleted);
        }

        [Fact]
        public void Uncomplete_SetsIsCompletedToFalse()
        {
            // Arrange
            var todoItem = new TodoItem(1, "Test Title");
            todoItem.Complete(); // Ensure it's completed first

            // Act
            todoItem.Uncomplete();

            // Assert
            Assert.False(todoItem.IsCompleted);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void UpdateTitle_InvalidTitle_ThrowsArgumentException(string newTitle)
        {
            // Arrange
            var todoItem = new TodoItem(1, "Test Title");

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => todoItem.UpdateTitle(newTitle));
            Assert.Equal("Title cannot be empty", exception.Message);
            Assert.Equal("newTitle", exception.ParamName);
        }

        [Fact]
        public void UpdateTitle_ValidTitle_UpdatesTitle()
        {
            // Arrange
            var todoItem = new TodoItem(1, "Old Title");
            string newTitle = "New Title";

            // Act
            todoItem.UpdateTitle(newTitle);

            // Assert
            Assert.Equal(newTitle, todoItem.Title);
        }
    }
}
