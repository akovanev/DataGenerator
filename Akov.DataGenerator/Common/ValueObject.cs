namespace Akov.DataGenerator.Common
{
    public class ValueObject
    {
        public ValueObject(string? name, object? value)
        {
            Name = name ?? "";
            Value = value;
        }


        public string Name { get; }
        public object? Value { get; }
    }
}