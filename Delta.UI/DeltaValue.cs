﻿namespace Delta.UI;


public abstract class DeltaValue(string name)
{
    public string Name { get; } = name;
    public abstract object? GetValue();
    public abstract string GetTypeOfValue();
    public abstract void SetValue(object? value);
}
public class DeltaValue<T>(string name, T value) : DeltaValue(name)
{
    public T Value { get; set; } = value;

    public override object? GetValue()
    {
        return Value;
    }
    
    public override string GetTypeOfValue()
    {
        return typeof(T).FullName!;
    }

    public override void SetValue(object? value)
    {
        Value = (T) value;
    }
}