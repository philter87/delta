namespace Delta.Html;

public abstract class HtmlNode
{
    public HtmlTag? Parent { get; set; }
    public int SiblingIndex { get; set; } = 0;
    public int Depth => Parent?.Depth + 1 ?? 0;
    
    public abstract IHtmlBuilder Render(IHtmlBuilder htmlBuilder);

    public string Render()
    {
        var builder = new HtmlBuilder();
        return Render(builder).ToString();
    }
    
    public static implicit operator HtmlNode(string text) => new HtmlTextNode(text);
    public static implicit operator HtmlNode(HtmlTagFunc func) => func();
    // public static implicit operator HtmlNode(List<HtmlNode> children) => new HtmlTag("span")[children.ToArray()];
    // public static implicit operator HtmlNode(List<HtmlTag> children) => new HtmlTag("span")[children.ToArray()];

}