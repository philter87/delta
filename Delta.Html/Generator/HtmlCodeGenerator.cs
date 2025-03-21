
namespace Delta.Html.Generator;

public static class HtmlCodeGenerator
{
    const string FactoryClassName = "Tags";
    const string BaseClass = nameof(HtmlTag);
    const string Namespace = "Delta.Html";

    public static void Main()
    {
        var sourceCode = $"""
                           namespace {Namespace};

                           {AddStaticMethodsToTagsClass(TagsMeta.Tags)}
                           
                           {AddTagClasses(TagsMeta.Tags)}
                           """;
        
        
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(),"..", "..", "..", FactoryClassName + ".cs");

        Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
        File.WriteAllText(outputPath, sourceCode);

        Console.WriteLine($"Generated: {outputPath}");
    }

    private static string AddTagClasses(IDictionary<string, List<string>> tags)
    {
        return string.Join("",tags.Select(kv => AddTagClass(kv.Key, kv.Value)));
    }

    private static string AddTagClass(string tag, List<string> attributesShorthand)
    {
        var tagClassName = tag.Substring(0, 1).ToUpper() + tag.Substring(1);
        var attributes = Convert(attributesShorthand);
        return $$"""

                 public class {{tagClassName}} : {{BaseClass}}
                 {
                     public {{tagClassName}}({{CreateParameters(attributes)}}) : base("{{tag}}")
                     {
                         this{{AddAttributes(attributes)}};
                     }
                 }
                 """;
    }
    

    private static string AddStaticMethodsToTagsClass(IDictionary<string, List<string>> tags)
    {
        return $$"""

                 public static class {{FactoryClassName}}
                 {
                 {{AddStaticMethods(tags)}}
                 }
                 """;
    }
    
    private static string AddStaticMethods(IDictionary<string, List<string>> tags)
    {
        return string.Join("", tags.Select(kv => AddStaticMethod(kv.Key, kv.Value)));
    }
    private static string AddStaticMethod(string tag, List<string> attributesShort)
    {
        var attributes = Convert(attributesShort);
        
        return $$"""
                     public static {{BaseClass}} {{tag}}({{CreateParameters(attributes)}})
                     {
                         return new {{BaseClass}}("{{tag}}"){{AddAttributes(attributes)}};
                     }
                 """;
    }

    private static string AddAttributes(List<HtmlAttribute> attributes)
    {
        return string.Join("", attributes.Select(attr => attr.AsAddAttribute()));
    }

    private static string CreateParameters(List<HtmlAttribute> attributes)
    {
        return string.Join(", ", attributes.Select(a => a.AsParameter()));
    }

    private static List<HtmlAttribute> Convert(List<string> attributes)
    {
        return attributes
            .Distinct()
            .Select(attr => new HtmlAttribute(attr))
            .ToList();
    }
    
    private sealed class HtmlAttribute
    {
        public HtmlAttribute(string shorthand)
        {
            var words = shorthand.Split('=');
            HtmlName = words[0];
            FieldName = HtmlName == "class" ? "classes" : HtmlName; 
            Type = words.Length == 1 ? "string" : words[1];
        }

        private string FieldName { get; }
        private string HtmlName { get; }
        private string Type { get; }

        public string AsParameter()
        {
            return $"{Type}? {FieldName} = null";
        }

        public string AsAddAttribute()
        {
            return $".{nameof(HtmlTag.With)}(\"{HtmlName}\",{FieldName})";
        }
    }
}