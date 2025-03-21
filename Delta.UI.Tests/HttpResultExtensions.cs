using System.Text.RegularExpressions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Delta.UI.Tests;

public static class ResultTestExtensions
{
    public static void ShouldHaveContentType(this IResult result, string contentType)
    {
        var contentResult = result.AsContentHttpResult();
        contentResult.ContentType.Should().Be(contentType);
    }

    public static void ShouldContain(this IResult result, string content)
    {
        var contentResult = result.AsContentHttpResult();
        contentResult.ResponseContent.Should().Contain(content);
    }
    
    public static void ShouldContainIgnoringScriptAndDeltaIds(this IResult result, string expectedContent)
    {
        var content = RemoveDataDeltaIdAndScripts(result.AsContentHttpResult().ResponseContent);
        content.Should().Contain(expectedContent, $"The content {expectedContent} should be part of {content}" );
    }
    
    
    private static string RemoveDataDeltaIdAndScripts(string html)
    {
        // Regex captures: data-delta-id="someValue"
        // and replaces it with nothing
        var contentWithoutDeltas = Regex.Replace(html, @" data-delta-id=""[^""]*""", "");
        var contentWithoutScripts = Regex.Replace(contentWithoutDeltas, @"<script[^>]*>[\s\S]*?</script>", "");
        return contentWithoutScripts;
    }

    private static ContentHttpResult AsContentHttpResult(this IResult result)
    {
        var contentResult = result as ContentHttpResult;
        contentResult.Should().NotBeNull("The result should be a ContentResult but found {0}", result.GetType().Name);
        return contentResult;
    }
}