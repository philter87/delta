using System.Reflection;
using System.Security.Cryptography;
using Delta.Html;

namespace Delta.UI;

public class Delta: HtmlNode
{
    private const string DeltaIdAttributeName = "data-delta-id";
    
    // Id used to identify the delta/element on the client side
    public string Id { get; }
    
    // The delta function that will be rendered
    private readonly Func<HttpContext, HtmlTag> _delta;
    
    // List of DeltaValues that are used in the delta function
    public List<DeltaValue> DeltaValues { get; set; } = [];

    public Delta(Func<HttpContext, HtmlTag> delta)
    {
        DeltaValues = GetDeltaValues(delta);
        Id = CreateId(delta.GetMethodInfo());
        _delta = delta;
    }

    private List<DeltaValue> GetDeltaValues(Func<HttpContext, HtmlTag> delta)
    {
        
        return GetDeltaValueFields(delta)
            .Select(f => (DeltaValue?)f.GetValue(delta.Target))
            .OfType<DeltaValue>()
            .ToList();
    }
    
    public override IHtmlBuilder Render(IHtmlBuilder htmlBuilder)
    {
        
        return _delta(GetContext(htmlBuilder))
            .With(DeltaIdAttributeName, Id)
            .Render(htmlBuilder);;
    }
    
    private List<FieldInfo> GetDeltaValueFields(Func<HttpContext, HtmlTag> delta)
    {
        if (delta.Target == null) return [];
        return delta.Target.GetType().GetFields()
            .Where(f => f.FieldType.IsGenericType && f.FieldType.GetGenericTypeDefinition() == typeof(DeltaValue<>))
            .ToList();
    }

    public string RenderWith(HttpContext context, List<DeltaValue> values)
    {
        if(_delta.Target == null) return "";
        foreach (var field in GetDeltaValueFields(_delta))
        {
            var deltaValue = (DeltaValue?) field.GetValue(_delta.Target);
            if (deltaValue == null) continue;
            var newDeltValue = values.Find(v => v.Name == deltaValue.Name);
            if (newDeltValue == null) continue;
            deltaValue.SetValue(newDeltValue.GetValue());
        }

        var builder = new HtmlBuilderWithContext(context);
        return Render(builder).ToString() ?? "";
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