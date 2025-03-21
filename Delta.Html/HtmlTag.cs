namespace Delta.Html;

public class HtmlTag(string tag) : HtmlNode
{
    private Dictionary<string, object> Attributes { get; } = new();
    public List<HtmlNode> Children { get; } = [];

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

            Children.AddRange(children);
            return this;
        }
    }

    public override IHtmlBuilder Render(IHtmlBuilder htmlBuilder)
    {
        htmlBuilder.Add($"<{tag}{CreateAttributes()}>");
        Children.ForEach(childElement => childElement.Render(htmlBuilder));
        return htmlBuilder.Add($"</{tag}>");
    }

    private string CreateAttributes()
    {
        return Attributes.Count == 0
            ? ""
            : " " + string.Join(" ", Attributes.Select(kv => kv.Key + "=\"" + kv.Value + "\""));
    }

    public HtmlTag Add(HtmlNode node)
    {
        Children.Add(node);
        return this;
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

    public List<HtmlNode> GetAllDescendants()
    {
        return Children
            .SelectMany(c => c is HtmlTag htmlTag 
                ? htmlTag.GetAllDescendants()
                : [c])
            .Append(this)
            .ToList();
    }

    public override string ToString()
    {
        return $"<{tag}{CreateAttributes()}>";
    }
}

public delegate HtmlTag HtmlTagFunc(params HtmlNode[] children);