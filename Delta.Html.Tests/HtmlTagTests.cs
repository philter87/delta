using FluentAssertions;
using static Delta.Html.Html;
using static Delta.Html.Tags;

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
    
    [Fact]
    public void GetDescendants_Should_ReturnAllChildren()
    {
        // Arrange
        var tag = div(id: "0") [// +1
            "Hi",               // +1
            p()["A", "B"],      // +3
            div(id:"1")[        // +1
                "World",        // +1
                p()["!"],       // +2
                p(),            // +1
                div()           // +1
            ]
        ];

        // Act
        var html = tag.Render();
        var descendants = tag.GetAllDescendants();
        
        // Assert
        descendants.Should().HaveCount(11);
        html.Should().Be(tag.Render());
    }
}