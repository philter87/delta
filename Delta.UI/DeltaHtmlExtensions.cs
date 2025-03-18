using Delta.Html;

namespace Delta.UI;

public static class DeltaHtmlExtensions
{
    public static IResult ToHtml(this HtmlTag htmlTag, HttpContext httpContext)
    {
        return Results.Content(htmlTag.Render().ToString(), "text/html");
    }
    
}