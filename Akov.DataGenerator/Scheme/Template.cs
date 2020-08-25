namespace Akov.DataGenerator.Scheme
{
    internal class Template
    {
        public TemplateType Type { get; set; }
        public string? Name { get; set; }
        public string? Pattern { get; set; }
    }

    internal enum TemplateType
    {
        String,
        Set,
        Double,
        Int,
        DateTime
    }
}