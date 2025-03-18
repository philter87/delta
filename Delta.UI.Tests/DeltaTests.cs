using FluentAssertions;
using static Delta.Html.Html;

namespace Delta.UI.Tests;

public class DeltaTests
{
    [Fact]
    public void Should_FindDependencies_When_UsedInLambdaFunction()
    {
        // Arrange
        var stateA = new DeltaState<string>("a", "val");
        var stateB = new DeltaState<int>("b", 3);

        // Act
        var delta = new Delta((c) => div()[
            stateA.Value,
            stateB.Value+""
        ]);

        // Assert
        delta.Dependencies.Should().HaveCount(2);
        delta.Dependencies[0].Name.Should().Be("a");
        delta.Dependencies[0].GetValue().Should().Be("val");
        delta.Dependencies[1].Name.Should().Be("b");
        delta.Dependencies[1].GetValue().Should().Be(3);
        delta.Id.Should().NotBeEmpty();
        var html = delta.Render(Any.HtmlBuilder()).ToString();
        html.Should().Be($"""<div data-delta-id="{delta.Id}">val3</div>""");
    }
}