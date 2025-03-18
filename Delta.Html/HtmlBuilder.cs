using System.Text;

namespace Delta.Html;

public class HtmlBuilder : IHtmlBuilder
{
    private readonly StringBuilder _builder = new(128);
    public IHtmlBuilder Add(string content)
    {
        _builder.Append(content);
        return this;
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
}