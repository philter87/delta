using System.Text;

namespace Delta.Html;

public class HtmlBuilder
{
    private readonly StringBuilder _builder = new(128);
    public HtmlBuilder AddLine(string rawContent)
    {
        _builder.Append(rawContent);
        return this;
    }

    public override string ToString()
    {
        return _builder.ToString();
    }
    
}