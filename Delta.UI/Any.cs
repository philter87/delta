using Delta.Html;

namespace Delta.UI;

public static class Any
{
    public static IHtmlBuilder HtmlBuilder()
    {
        return new HtmlBuilderWithContext(new DefaultHttpContext());
    }
}