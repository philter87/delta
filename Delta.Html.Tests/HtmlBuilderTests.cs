
using FluentAssertions;
using static Delta.Html.Tags;

namespace Delta.Html.Tests;

public class HtmlBuilderTests
{
    [Fact]
    public void Should_ReturnValidHtml_When_UsingHtmlTagsAndText()
    {
        // Arrange
        var div = new HtmlTag("div")[
            new HtmlTag("p")["Apple"],
            "Hello, World!"
        ];
        
        // Act
        var html = div.Render();
        
        // Assert
        html.Should().Be("<div><p>Apple</p>Hello, World!</div>");
    }
    
    [Fact]
    public void Should_ReturnValidHtml_When_UsingHtmlTagsAndAttributes()
    {
        // Arrange
        var div = new HtmlTag("div").With("class","no-class")[
            "Hello, World!"
        ];
        
        // Act
        var html = div.Render();
        
        // Assert
        html.Should().Be("""<div class="no-class">Hello, World!</div>""");
    }
    
    [Fact]
    public void Should_ReturnValidHtml_When_UsingStaticMethods()
    {
        // Arrange
        var tag = div("no-class", style: "width: 10px")[
            p()["Hello"],
            "World!"
        ];
        
        // Act
        var html = tag.Render();
        
        // Assert
        html.Should().Be("""<div class="no-class" style="width: 10px"><p>Hello</p>World!</div>""");
    }
}