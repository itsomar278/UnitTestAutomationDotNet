using System;
using Xunit;
using TodoList;

public class TodoCategoryTests
{
    [Fact]
    public void Constructor_ValidParameters_CreatesCategory()
    {
        // Arrange
        var name = "Work";
        var description = "Work related tasks";
        var color = "#FF5733";

        // Act
        var category = new TodoCategory(name, description, color);

        // Assert
        Assert.NotNull(category);
        Assert.Equal(name, category.Name);
        Assert.Equal(description, category.Description);
        Assert.Equal(color, category.Color);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_InvalidName_ThrowsArgumentException(string invalidName)
    {
        // Arrange
        var description = "Description";
        var color = "#FFFFFF";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new TodoCategory(invalidName, description, color));
    }

    [Theory]
    [InlineData("#FF5733", true)]
    [InlineData("#FFF", true)]
    [InlineData("FF5733", false)]
    [InlineData("#GHIJKL", false)]
    public void ValidateColor_ValidAndInvalidColors_ReturnsExpectedResult(string color, bool expectedResult)
    {
        // Act
        var result = TodoCategory.ValidateColor(color);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public void UpdateName_ValidName_UpdatesName()
    {
        // Arrange
        var category = new TodoCategory("Original Name", "Description", "#FFFFFF");
        var newName = "Updated Name";

        // Act
        category.UpdateName(newName);

        // Assert
        Assert.Equal(newName, category.Name);
    }

    [Fact]
    public void UpdateName_InvalidName_ThrowsArgumentException()
    {
        // Arrange
        var category = new TodoCategory("Original Name", "Description", "#FFFFFF");
        var invalidName = "";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => category.UpdateName(invalidName));
    }
}