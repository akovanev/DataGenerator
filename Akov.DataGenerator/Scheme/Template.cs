namespace Akov.DataGenerator.Scheme
{
    public class Template
    {
        public string? Type { get; set; }
        public string? Name { get; set; }
        public string? Pattern { get; set; }
    }

    public class TemplateType
    {
        public const string String = "string";
        public const string Set = "set";
        public const string Bool = "bool";
        public const string Int = "int";
        public const string Double = "double";
        public const string DateTime = "datetime";
        public const string Array = "array";
        public const string Object = "object";
    }
}