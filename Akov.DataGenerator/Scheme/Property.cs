namespace Akov.DataGenerator.Scheme
{
    public class Property
    {
        public string? Type { get; set; }
        public string? Pattern { get; set; }
        public string? SequenceSeparator { get; set; }
        public string? Name { get; set; }
        public int? MinLength { get; set; } 
        public int? MaxLength { get; set; }
        public int? MinSpaceCount { get; set; }
        public int? MaxSpaceCount { get; set; }
        public object? MinValue { get; set; }
        public object? MaxValue { get; set; }
        public Failure? Failure { get; set; }
        public string? CustomFailure { get; set; }
    }

    public class Failure
    {
        public double? Nullable { get; set; }
        public double? Custom { get; set; }
        public double? Range { get; set; }
    }
}