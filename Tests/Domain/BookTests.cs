using Domain.Entities;
using System;
using Xunit;

namespace Tests.Domain
{
    public class BookTests
    {
        [Fact]
        public void Constructor_InitializesDescriptionToEmptyString()
        {
            // Arrange & Act
            var book = new Book(Guid.NewGuid(), "Test Title", "Test Author", "1234567890", 2020);

            // Assert
            Assert.Equal(string.Empty, book.Description);
        }

        [Fact]
        public void UpdateDescription_SuccessfullyUpdatesDescription()
        {
            // Arrange
            var book = new Book(Guid.NewGuid(), "Test Title", "Test Author", "1234567890", 2020);
            var newDescription = "This is a test description.";

            // Act
            book.UpdateDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, book.Description);
        }

        [Fact]
        public void UpdateDescription_ThrowsArgumentNullException_ForNullInput()
        {
            // Arrange
            var book = new Book(Guid.NewGuid(), "Test Title", "Test Author", "1234567890", 2020);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => book.UpdateDescription(null));
        }
    }
}
