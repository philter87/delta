using Delta.Html;
using Delta.UI.Dto;
using FluentAssertions;
using static Delta.Html.Tags;

namespace Delta.UI.Tests;

public class DeltaHtmlExtensionsTests
{
    private const string DeltaValueIdA = "deltaA";
    private const string DeltaValueIdB = "deltaB";
    [Fact]
    public void GetDeltas_Should_ReturnAllDeltas()
    {
        // Arrange
        var page = CreatePage();
        
        // Act
        var deltas = page.GetDeltas();
        var deltaValues = page.GetDeltaValues();
        
        // Aassert
        deltas.Should().HaveCount(2);
        deltaValues.Should().HaveCount(2);
    }

    [Fact]
    public void ToHtml_Should_RenderEntireHtmlPage()
    {
        // Arrange
        var page = CreatePage();
        
        // Act
        var htmlPage = page.ToHtml(Any.HttpContext());

        // Assert
        htmlPage.ShouldHaveContentType("text/html");
        htmlPage.ShouldContainIgnoringScriptAndDeltaIds("<html><body><div>Plus: 3</div><div>Minus: -1</div></body></html>");
    }
    
    [Fact]
    public void ToHtml_Should_RenderDeltas()
    {
        // Arrange
        var page = CreatePage();
        var deltaContext = Any.HttpContextDelta(new ClientStateDto());
        
        // Act
        var htmlPage = page.ToHtml(deltaContext);

        // Assert
        htmlPage.ShouldHaveContentType("application/json");
    }
    
    [Fact]
    public void ToHtml_Should_IncludeDeltaRuntime()
    {
        // Arrange
        var page = CreatePage();
        
        // Act
        var htmlPage = page.ToHtml(Any.HttpContext());

        // Assert
        htmlPage.ShouldHaveContentType("text/html");
        htmlPage.ShouldContain(JavascriptLoader.DeltaRuntime);
    }
    
    [Fact]
    public void ToHtml_Should_IncludeState()
    {
        // Arrange
        var page = CreatePage();
        
        // Act
        var htmlPage = page.ToHtml(Any.HttpContext());

        // Assert
        htmlPage.ShouldHaveContentType("text/html");
        htmlPage.ShouldContain("__clientState");
        htmlPage.ShouldContain(DeltaValueIdA);
        htmlPage.ShouldContain(DeltaValueIdB);
    }

    private HtmlTag CreatePage()
    {
        var deltaValueA = new DeltaValue<int>(DeltaValueIdA, 1);
        var deltaValueB = new DeltaValue<int>(DeltaValueIdB, 2);
        return html()[
            body()[
                new Delta(c => div()[
                    "Plus: ", deltaValueA.Value + deltaValueB.Value
                ]),
                new Delta(c => div()[
                    "Minus: ", deltaValueA.Value - deltaValueB.Value
                ])
            ]
        ];
    }
}