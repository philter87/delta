using FluentAssertions;
using static Delta.Html.Html;

namespace Delta.Html.Tests;

public class HtmlTagTests
{
    [Fact]
    public void Should_SetParent_When_AddingChild()
    {
        // Arrange
        var child = p()["World"];
        var tag = div() [
            "Hi",
            p()["Hello"],
            child
        ];

        // Act
        
        // Assert
        tag.Parent.Should().BeNull();
        tag.Depth.Should().Be(0);
        child.Depth.Should().Be(1);
        child.SiblingIndex.Should().Be(2);
        child.Parent.Should().Be(tag);
    }
}