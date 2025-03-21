namespace Delta.UI;

public static class LoadFlashRuntime
{
    private const string NamespaceDirectory = "Delta.UI.Resources.";
    public static readonly string DeltaJavascriptRuntime = ReadResource("DeltaRuntime.js");
    
    private static string ReadResource(string fileName)
    {
        var namespacePath = NamespaceDirectory + fileName;
        var stream = typeof(LoadFlashRuntime).Assembly.GetManifestResourceStream(namespacePath);
        return new StreamReader(stream).ReadToEnd();
    }
}