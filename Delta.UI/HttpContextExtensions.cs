namespace Delta.UI;

public static class HttpContextExtensions
{
    public static bool IsDeltaRequest(this HttpContext httpContext)
    {
        return httpContext.Request.Headers["X-Delta"].Contains("true");
    }
}