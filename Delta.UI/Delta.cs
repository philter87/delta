using System.Reflection;
using System.Security.Cryptography;
using Delta.Html;

namespace Delta.UI;

public class Delta: HtmlNode
{
    public const string DeltaIdAttributeName = "data-delta-id";
    public string Id { get; }
    private readonly Func<HttpContext, HtmlTag> _delta;
    public List<DeltaState> Dependencies { get; set; } = [];

    public Delta(Func<HttpContext, HtmlTag> delta)
    {
        Dependencies = GetDependencies(delta);
        Id = CreateId(delta.GetMethodInfo());
        _delta = delta;
    }

    private List<DeltaState> GetDependencies(Func<HttpContext, HtmlTag> delta)
    {
        if (delta.Target == null) return [];
        return delta.Target.GetType().GetFields()
            .Where(f => f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(DeltaState<>))
            .Select(f => (DeltaState?)f.GetValue(delta.Target))
            .OfType<DeltaState>().ToList();
    }
    
    public override IHtmlBuilder Render(IHtmlBuilder htmlBuilder)
    {
        return _delta(GetContext(htmlBuilder))
            .With(DeltaIdAttributeName, Id)
            .Render(htmlBuilder);;
    }
    
    public HttpContext? GetContext(IHtmlBuilder htmlBuilder)
    {
        return (htmlBuilder as HtmlBuilderWithContext)?.HttpContext;
    }
    
    public static string CreateId(MethodInfo methodInfo)
    {
        var ilCode = methodInfo.GetMethodBody().GetILAsByteArray();
        var hashBytes = MD5.HashData(ilCode);
        return Convert.ToBase64String(hashBytes, 0, 6);
    } 
}