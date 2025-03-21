using System.Text.Json;
using Delta.Html;
using Delta.UI.Dto;
using Microsoft.AspNetCore.Http;

namespace Delta.UI.Tests;

public static class Any
{
    public static IHtmlBuilder HtmlBuilder()
    {
        return new HtmlBuilderWithContext(HttpContext());
    }
    
    public static HttpContext HttpContext()
    {
        return new DefaultHttpContext();
    }

    public static HttpContext HttpContextDelta(ClientStateDto state)
    {
        var context = new DefaultHttpContext();
        
        var stream = new MemoryStream();
        JsonSerializer.Serialize(stream, state);
        stream.Position = 0;
        context.Request.Body = stream;
        context.Request.Headers[Constants.DeltaHeader] = Constants.DeltaValue;
        return context;
    }
}