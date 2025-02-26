namespace Akov.DataGenerator.Models;

public class NameValueObject(string? name, object? value)
{
    public string Name { get; } = name ?? "";
    public object? Value { get; } = value;
}