using System.Text.RegularExpressions;

namespace Akov.DataGenerator.Utilities;

/// <summary>
/// Provides functionality to process a template string by replacing specific placeholders 
/// with random values, resource values, or file content.
/// </summary>
public static class TemplateProcessor
{
    private static readonly ResourceReader ResourceReader = new();
    
    /// <summary>
    /// Processes the provided template string by replacing placeholders with random values.
    /// Supports placeholders for number ranges, resources, and file content.
    /// </summary>
    /// <param name="random">The random number generator used to generate random values.</param>
    /// <param name="template">The template string containing placeholders to be replaced.</param>
    /// <returns>The processed template with placeholders replaced by generated values.</returns>
    /// <exception cref="FileNotFoundException">Thrown if a file specified in the template is not found.</exception>
    public static string CreateValue(Random random, string template)
    {
        // Replace [number:X-Y]
        template = Regex.Replace(template, @"\[number:(\d+)-(\d+)\]", match =>
        {
            int min = int.Parse(match.Groups[1].Value);
            int max = int.Parse(match.Groups[2].Value);
            return random.Next(min, max + 1).ToString();
        });
        
        template = Regex.Replace(template, @"\[oneof:([^\]]+)\]", match =>
        {
            var options = match.Groups[1].Value.Split(',').Select(s => s.Trim()).ToArray();
            return options[random.Next(options.Length)];
        });

        // Replace [resource:Name]
        template = Regex.Replace(template, @"\[resource:([A-Za-z]+)(?::(\d+)-(\d+))?\]", match =>
        {
            string key = match.Groups[1].Value;
            var words = ResourceReader.ReadEmbeddedResource(key).Split(",");
            return GetRandomSubstring(match, random, words);
        });

        // Replace [file:path]
        template = Regex.Replace(template, @"\[file:([^\]:]+)(?::(\d+)-(\d+))?\]", match =>
        {
            string path = match.Groups[1].Value;
            if (!File.Exists(path)) 
                throw new FileNotFoundException($"File '{path}' not found.");
            var words = Dependencies.Factories.FileHelper.Value.GetFileContent(path).Split(',');
            return GetRandomSubstring(match, random, words);
        });

        return template;
    }
    
    private static string GetRandomSubstring(Match match, Random random, string[] words)
    {
        if (match.Groups[2].Success && match.Groups[3].Success) // If range is specified
        {
            int min = int.Parse(match.Groups[2].Value);
            int max = int.Parse(match.Groups[3].Value);
            words = words.Where(word => word.Length >= min && word.Length <= max).ToArray();
        }

        return words.Any() 
            ? words[random.Next(words.Length)]
            : throw new InvalidOperationException("No suitable word found within the specified range.");
    }
}