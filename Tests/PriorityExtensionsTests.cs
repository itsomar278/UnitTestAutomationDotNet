using System;
using Xunit;
using TodoList;

public class PriorityExtensionsTests
{
    [Theory]
    [InlineData(Priority.None, "No Priority")]
    [InlineData(Priority.Low, "Low")]
    [InlineData(Priority.Medium, "Medium")]
    [InlineData(Priority.High, "High")]
    [InlineData(Priority.Critical, "Critical")]
    public void GetDisplayName_ValidPriority_ReturnsExpectedDisplayName(Priority priority, string expectedDisplayName)
    {
        // Act
        var displayName = PriorityExtensions.GetDisplayName(priority);

        // Assert
        Assert.Equal(expectedDisplayName, displayName);
    }

    [Theory]
    [InlineData(Priority.None, false)]
    [InlineData(Priority.Low, false)]
    [InlineData(Priority.Medium, false)]
    [InlineData(Priority.High, true)]
    [InlineData(Priority.Critical, true)]
    public void IsUrgent_ValidPriority_ReturnsExpectedUrgency(Priority priority, bool expectedUrgency)
    {
        // Act
        var isUrgent = PriorityExtensions.IsUrgent(priority);

        // Assert
        Assert.Equal(expectedUrgency, isUrgent);
    }

    [Theory]
    [InlineData(Priority.None, 4)]
    [InlineData(Priority.Low, 3)]
    [InlineData(Priority.Medium, 2)]
    [InlineData(Priority.High, 1)]
    [InlineData(Priority.Critical, 0)]
    public void GetSortOrder_ValidPriority_ReturnsExpectedSortOrder(Priority priority, int expectedSortOrder)
    {
        // Act
        var sortOrder = PriorityExtensions.GetSortOrder(priority);

        // Assert
        Assert.Equal(expectedSortOrder, sortOrder);
    }
}