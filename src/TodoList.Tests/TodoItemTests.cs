using System;
using Xunit;

namespace TodoList.Tests
{
    public class TodoItemTests
    {
        [Fact]
        public void Constructor_WithValidParameters_InitializesPropertiesCorrectly()
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
        public void Constructor_WithEmptyDescription_InitializesDescriptionAsEmpty()
        {
            // Arrange
            int id = 2;
            string title = "Another Test Title";

            // Act
            var todoItem = new TodoItem(id, title);

            // Assert
            Assert.Equal(string.Empty, todoItem.Description);
        }

        [Fact]
        public void Complete_SetsIsCompletedToTrue()
        {
            // Arrange
            var todoItem = new TodoItem(3, "Complete Test");

            // Act
            todoItem.Complete();

            // Assert
            Assert.True(todoItem.IsCompleted);
        }

        [Fact]
        public void Uncomplete_SetsIsCompletedToFalse()
        {
            // Arrange
            var todoItem = new TodoItem(4, "Uncomplete Test");
            todoItem.Complete(); // Ensure it's completed first

            // Act
            todoItem.Uncomplete();

            // Assert
            Assert.False(todoItem.IsCompleted);
        }

        [Theory]
        [InlineData("New Description")]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateDescription_UpdatesDescriptionCorrectly(string newDescription)
        {
            // Arrange
            var todoItem = new TodoItem(5, "Description Test");

            // Act
            todoItem.UpdateDescription(newDescription);

            // Assert
            string expectedDescription = newDescription ?? string.Empty;
            Assert.Equal(expectedDescription, todoItem.Description);
        }
    }
}