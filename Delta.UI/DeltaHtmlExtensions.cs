using System.Text.Json;
using Delta.Html;
using Delta.UI.Dto;

namespace Delta.UI;

public static class DeltaHtmlExtensions
{
    public static IResult ToHtml(this HtmlTag htmlTag, HttpContext httpContext)
    {
        var builder = new HtmlBuilderWithContext(httpContext);
        if (httpContext.IsDeltaRequest())
        {
            var sharedState = JsonSerializer.DeserializeAsync<ClientStateDto>(httpContext.Request.Body).Result;
            if(sharedState == null) throw new Exception("Client state should not be null");
            
            var clientRenders = htmlTag.GetClientDeltaRenders(sharedState, httpContext);
            var clientActions = new ClientActionsDto(clientRenders);
            var json = JsonSerializer.Serialize(clientActions);
            return Results.Content(json, "application/json");
        }
        
        htmlTag.Add(Tags.script()[JavascriptLoader.DeltaRuntime]);
        htmlTag.AddClientState();
        return Results.Content(htmlTag.Render(builder).ToString(), "text/html");
    }
    
    private static HtmlTag AddClientState(this HtmlTag htmlTag)
    {
        var state = ClientStateDto.CreateInitialState(htmlTag);
        var json = JsonSerializer.Serialize(state);
        var script = Tags.script()["__clientState = " + json + ";"];
        return htmlTag.Add(script);
    }

    public static List<Delta> GetDeltas(this HtmlTag htmlTag)
    {
        return htmlTag.GetAllDescendants().OfType<Delta>().ToList();
    }
    
    public static List<ClientDeltaRender> GetClientDeltaRenders(this HtmlTag htmlTag, ClientStateDto stateDto, HttpContext httpContext)
    {
        var deltaIdsToRender = stateDto.GetDeltaIds();
        var deltaValues = stateDto.DeltaValues.Select(d => d.ToDeltaValue()).ToList();
        return htmlTag.GetDeltas()
            .Where(d => deltaIdsToRender.Contains(d.Id))
            .Select(d => new ClientDeltaRender(d.Id, d.RenderWith(httpContext, deltaValues)))
            .ToList(); 
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