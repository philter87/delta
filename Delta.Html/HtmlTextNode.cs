namespace Delta.Html;

public class HtmlTextNode(string rawContent) : HtmlNode
{
    public override IHtmlBuilder Render(IHtmlBuilder htmlBuilder)
    {
        return htmlBuilder.Add(rawContent);
    }

    public override string ToString()
    {
        return rawContent;
    }
}