using System.Text.Json;
using Delta.Html;
using Delta.UI.Dto;

namespace Delta.UI;

public static class DeltaHtmlExtensions
{
    internal static IResult ToHtml(this HtmlTag htmlTag, HttpContext httpContext)
    {
        var builder = new HtmlBuilderWithContext(httpContext);
        if (httpContext.IsDeltaRequest())
        {
            var sharedState = JsonSerializer.DeserializeAsync<ClientStateDto>(httpContext.Request.Body).Result;
            
            var state = ClientStateDto.From(htmlTag);
            var json = JsonSerializer.Serialize(state);
            return Results.Content(json, "application/json");
        }
        htmlTag.Add(Tags.script()[LoadFlashRuntime.DeltaJavascriptRuntime]);
        htmlTag.AddClientState();
        return Results.Content(htmlTag.Render(builder).ToString(), "text/html");
    }
    
    private static HtmlTag AddClientState(this HtmlTag htmlTag)
    {
        var state = ClientStateDto.From(htmlTag);
        var json = JsonSerializer.Serialize(state);
        var script = Tags.script()["__clientState = " + json + ";"];
        return htmlTag.Add(script);
    }

    public static List<Delta> GetDeltas(this HtmlTag htmlTag)
    {
        return htmlTag.GetAllDescendants().OfType<Delta>().ToList();
    }
    
    public static List<DeltaValue> GetDeltaValues(this HtmlTag htmlTag)
    {
        return htmlTag.GetDeltas()
            .SelectMany(d => d.DeltaValues)
            .GroupBy(g => g.Name)
            .Select(g => g.First())
            .ToList();
    }
}