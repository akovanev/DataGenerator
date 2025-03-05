namespace Akov.DataGenerator.Utilities;

public record FileReadConfig(bool UseCache);

internal class FileHelper(FileReadConfig? config = null)
{
    private readonly bool _useCache = config?.UseCache ?? false;
    private readonly Dictionary<string, string> _cachedContent = new();

    public string GetFileContent(string filename)
    {
        if (_useCache && _cachedContent.TryGetValue(filename, out var content))
            return content;

        using var reader = new StreamReader(filename);

        if (!_useCache)
            return reader.ReadToEnd();

        _cachedContent.Add(filename, reader.ReadToEnd());
        return _cachedContent[filename];
    }
}

