namespace Akov.DataGenerator.Scheme
{
    internal class Property
    {
        public string Template { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int? MinLength { get; set; }
        public int? MaxLength { get; set; }
        public int? MinSpaceCount { get; set; }
        public int? MaxSpaceCount { get; set; }
        public object? MinValue { get; set; }
        public object? MaxValue { get; set; }
        public Failure? Failure { get; set; }
    }

    internal class Failure
    {
        public int? Nullable { get; set; }
        public int? Invalid { get; set; }
    }
}