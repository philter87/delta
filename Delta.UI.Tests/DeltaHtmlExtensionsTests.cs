using FluentAssertions;
using static Delta.Html.Tags;

namespace Delta.UI.Tests;

public class DeltaHtmlExtensionsTests
{
    [Fact]
    public void GetDeltas_Should_ReturnAllDeltas()
    {
        // Arrange
        var deltaValueA = new DeltaValue<int>("a", 1);
        var deltaValueB = new DeltaValue<int>("b", 2);
        var tags = html()[
            body()[
                new Delta(c => div()[
                    "Plus: ", deltaValueA.Value + deltaValueB.Value
                ]),
                new Delta(c => div()[
                    "Minus: ", deltaValueA.Value - deltaValueB.Value
                ])
            ]
        ];
        
        // Act
        var deltas = tags.GetDeltas();
        var deltaValues = tags.GetDeltaValues();
        
        // Aassert
        deltas.Should().HaveCount(2);
        deltaValues.Should().HaveCount(2);
    }
}