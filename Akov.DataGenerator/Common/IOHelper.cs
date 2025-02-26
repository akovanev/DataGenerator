using System.Collections.Generic;
using System.IO;

namespace Akov.DataGenerator.Common;

/// <summary>
/// The wrapper on the StreamReader and StreamWriter.
/// </summary>
// ReSharper disable once InconsistentNaming
public class IOHelper(FileReadConfig? config = null)
{
    private readonly bool _useCache = config?.UseCache ?? false;
    private readonly Dictionary<string, string> _cachedContent = new();

    public string GetFileContent(string filename)
    {
        if (_useCache && _cachedContent.TryGetValue(filename, out var content))
            return content;
            
        using var reader = new StreamReader(filename);
            
        if(!_useCache)
            return reader.ReadToEnd();
            
        _cachedContent.Add(filename, reader.ReadToEnd());
        return _cachedContent[filename];
    }

    public void SaveData(string filename, string data)
    {
        using var writer = new StreamWriter(filename);
        writer.WriteLine(data);
    }
}