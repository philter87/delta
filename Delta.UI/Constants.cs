namespace Delta.UI;

public static class Constants
{
    public const string DeltaHeader = "X-Delta";
    public const string DeltaValue = "true";
    
    public const string DataDeltaId = "data-delta-id";
    public static string DataDeltaIdWith(string id) => $"{DataDeltaId}=\"{id}\"";
}