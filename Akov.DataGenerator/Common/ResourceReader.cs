using System;
using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using Akov.DataGenerator.Extensions;

namespace Akov.DataGenerator.Common;

public class ResourceReader
{
    public string? ReadEmbeddedResource(string? resourceName)
    {
        resourceName.ThrowIfNullOrEmpty(nameof(resourceName));
        
        string[] split = resourceName!.Split(",");

        if (split.Length != 2)
            throw new ArgumentException("Resource name should be of format 'type,namespace.resource'");
        
        string assemblyType = split[0];
        string resource = split[1]; 

        var assembly = GetAssembly(assemblyType);
        
        using var resourceStream = assembly.GetManifestResourceStream(resource);
        if (resourceStream is null) return null;
        
        using var streamReader = new StreamReader(resourceStream);
        return streamReader.ReadToEnd();
    }

    protected virtual Assembly GetAssembly(string typeName)
    {
        if (typeName != nameof(DG))
            throw new ArgumentException($"{nameof(ResourceReader)} expects {nameof(DG)} type");
        
        return Assembly.GetExecutingAssembly();
    }
}