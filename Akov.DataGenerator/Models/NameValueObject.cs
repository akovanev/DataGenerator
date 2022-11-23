namespace Akov.DataGenerator.Models;

public class NameValueObject
{
    public NameValueObject(string? name, object? value)
    {
        Name = name ?? "";
        Value = value;
    }

    public string Name { get; }
    public object? Value { get; }  
}