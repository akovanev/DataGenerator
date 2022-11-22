using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Akov.DataGenerator.Extensions;

namespace Akov.DataGenerator.Common;

public class ResourceReader
{
    private readonly Dictionary<string, string> _cachedContent = new();
    
    public string? ReadEmbeddedResource(string? resourceName)
    {
        resourceName.ThrowIfNullOrEmpty(nameof(resourceName));
        
        if(_cachedContent.TryGetValue(resourceName!, out var content))
        {
            return content;
        }
        
        string[] split = resourceName!.Split(",");

        if (split.Length != 2)
            throw new ArgumentException("Resource name should be of format 'type,namespace.resource'");
        
        string assemblyType = split[0];
        string resource = split[1]; 

        var assembly = GetAssembly(assemblyType);
        
        using var resourceStream = assembly.GetManifestResourceStream(resource);
        if (resourceStream is null) return null;
        
        using var streamReader = new StreamReader(resourceStream);
        content = streamReader.ReadToEnd();
        _cachedContent.Add(resourceName, content);
        
        return content;
    }

    private static Assembly GetAssembly(string typeName)
    {
        var type = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Single(t => string.Equals(t.Name, typeName, StringComparison.OrdinalIgnoreCase));
        
        return type.Assembly;
    }
}