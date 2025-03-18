namespace Delta.Html;

public class HtmlTag(string tag) : HtmlNode
{
    private Dictionary<string, object> Attributes { get; } = new();
    private readonly List<HtmlNode> _children = [];

    public HtmlTag this[params HtmlNode[] children]
    {
        get
        {
            for (var index = 0; index < children.Length; index++)
            {
                var child = children[index];
                child.SiblingIndex = index;
                child.Parent = this;
            }
            _children.AddRange(children);
            return this;
        }
    }

    public override IHtmlBuilder Render(IHtmlBuilder htmlBuilder)
    {
        htmlBuilder.Add($"<{tag}{CreateAttributes()}>");
        _children.ForEach(childElement => childElement.Render(htmlBuilder));
        return htmlBuilder.Add($"</{tag}>");
    }
    
    private string CreateAttributes()
    {
        return Attributes.Count == 0 
            ? "" 
            : " " + string.Join(" ", Attributes.Select(kv => kv.Key + "=\"" + kv.Value + "\""));
    }

    public HtmlTag With(string name, object? value)
    {
        if (value == null)
        {
            return this;
        }
        Attributes[name] = value;
        return this;
    }
}

public delegate HtmlTag HtmlTagFunc (params HtmlNode[] children);