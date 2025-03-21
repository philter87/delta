namespace Delta.UI;

public static class JavascriptLoader
{
    private const string NamespaceDirectory = "Delta.UI.Resources.";
    public static readonly string DeltaRuntime = ReadResource("DeltaRuntime.js");
    
    private static string ReadResource(string fileName)
    {
        var namespacePath = NamespaceDirectory + fileName;
        var stream = typeof(JavascriptLoader).Assembly.GetManifestResourceStream(namespacePath);
        return new StreamReader(stream).ReadToEnd();
    }
}