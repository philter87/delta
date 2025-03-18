namespace Delta.UI;


public abstract class DeltaState(string name)
{
    public string Name { get; } = name;
    public abstract object? GetValue();
}
public class DeltaState<T>(string name, T value) : DeltaState(name)
{
    public T Value { get; set; } = value;

    public override object? GetValue()
    {
        return Value;
    }
}