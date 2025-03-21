using System.Text.Json;
using Delta.Html;

namespace Delta.UI.Dto;

public class ClientStateDto
{
    public List<DeltaDto> Deltas { get; set; } = [];
    public List<DeltaValueDto> DeltaValues { get; set; } = [];
    
    public List<string> GetDeltaIds()
    {
        return Deltas.Select(d => d.Id).ToList();
    }
    
    public static ClientStateDto CreateInitialState(HtmlTag rootHtml)
    {
        return new ClientStateDto()
        {
            Deltas = rootHtml.GetDeltas().Select(DeltaDto.From).ToList(),
            DeltaValues = rootHtml.GetDeltaValues().Select(DeltaValueDto.From).ToList()
        };
    }
}

public class DeltaAction
{
    public required string DeltaId { get; set; }
    public List<DeltaValue> DeltaValues { get; set; } = [];
}

public class DeltaDto
{
    public required string Id { get; set; }
    public List<string> DeltaValueNames { get; set; } = [];

    public static DeltaDto From(Delta delta)
    {
        return new DeltaDto()
        {
            Id = delta.Id,
            DeltaValueNames = delta.DeltaValues.Select(dv => dv.Name).ToList()
        };
    }
}

public record DeltaValueDto(string Name, object? Value, string TypeName)
{
    public static DeltaValueDto From(DeltaValue dValue)
    {
        return new DeltaValueDto(dValue.Name, dValue.GetValue(), dValue.GetTypeOfValue());
    }
    
    public DeltaValue ToDeltaValue()
    {
        return new DeltaValue<object?>(Name, ConvertValue());
    }

    private object? ConvertValue()
    {
        if (Value is not JsonElement je) return null;
        var type = Type.GetType(TypeName);
        return type == null 
            ? null 
            : je.Deserialize(type);
    }
}