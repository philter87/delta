namespace Delta.Html;

public class HtmlTextNode : HtmlNode
{
    private readonly string _rawContent;

    public HtmlTextNode(string rawContent)
    {
        _rawContent = rawContent;
    }

    public override HtmlBuilder Render(HtmlBuilder htmlBuilder)
    {
        return htmlBuilder.AddLine(_rawContent);
    }
}