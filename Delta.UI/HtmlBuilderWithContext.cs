using Delta.Html;

namespace Delta.UI;

public class HtmlBuilderWithContext(HttpContext httpContext) : IHtmlBuilder
{
    private readonly HtmlBuilder _builder = new();
    public HttpContext HttpContext { get; } = httpContext;

    public IHtmlBuilder Add(string content)
    {
        return _builder.Add(content);
    }
    
    public override string ToString()
    {
        return _builder.ToString();
    }
}