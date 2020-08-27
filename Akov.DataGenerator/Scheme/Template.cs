namespace Akov.DataGenerator.Scheme
{
    public class Template
    {
        public TemplateType Type { get; set; }
        public string? Name { get; set; }
        public string? Pattern { get; set; }
    }

    public enum TemplateType
    {
        String,
        Set,
        Bool,
        Int,
        Double,
        DateTime,
        Array,
        Object
    }
}