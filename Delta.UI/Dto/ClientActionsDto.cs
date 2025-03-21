namespace Delta.UI.Dto;

public class ClientActionsDto(List<ClientDeltaRender> renders)
{
    public List<ClientDeltaRender> Renders { get; init; } = renders;
}

public class ClientDeltaRender(string deltaId, string html)
{
    public string DeltaId { get; init; } = deltaId;
    public string Html { get; init; } = html;

    public void Deconstruct(out string DeltaId, out string Html)
    {
        DeltaId = this.DeltaId;
        Html = this.Html;
    }
}