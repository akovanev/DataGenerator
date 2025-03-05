using System.Collections.Concurrent;
using System.Reflection;

namespace Akov.DataGenerator.Utilities;

internal class ResourceReader
{
    private const string FullResourceNameFormat = "Akov.DataGenerator.Resources.{0}.txt";
    private readonly ConcurrentDictionary<string, string> _cachedContent = new();

    public string ReadEmbeddedResource(string resourceName)
    {
        if(_cachedContent.TryGetValue(resourceName, out var content))
            return content ?? throw new InvalidOperationException($"Resource {resourceName} should not be null");

        var assembly = Assembly.GetExecutingAssembly();
        string resource = string.Format(FullResourceNameFormat, resourceName);

        using var resourceStream = assembly.GetManifestResourceStream(resource);
        if (resourceStream is null) throw new InvalidOperationException($"Manifest {resourceStream} should not be null");
        
        using var streamReader = new StreamReader(resourceStream);
        content = streamReader.ReadToEnd();
        _cachedContent.TryAdd(resourceName, content);
        
        return content;
    }
}