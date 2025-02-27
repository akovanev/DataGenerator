using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.Processors;
using Akov.DataGenerator.Profiles;
using Akov.DataGenerator.RunBehaviors;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Serializers;
using Newtonsoft.Json;

namespace Akov.DataGenerator;

public class DG(
    IGeneratorFactory? generatorFactory = null,
    DataSchemeMapperConfig? mapperConfig = null,
    FileReadConfig? fileReadConfig = null,
    IRunBehavior? runBehavior = null)
{
    private readonly IGeneratorFactory _generatorFactory = generatorFactory ?? new GeneratorFactory();
    private readonly Lazy<IOHelper> _ioHelper = new(() => new IOHelper(fileReadConfig));
    private readonly Lazy<DataSchemeMapper> _mapper = new(() => new DataSchemeMapper(mapperConfig));
    private readonly IRunBehavior _runBehavior = runBehavior ?? new NullRunBehavior();

    public DataScheme GetFromFile(string filename)
    {
        string fileContent = _ioHelper.Value.GetFileContent(filename);
        return JsonConvert.DeserializeObject<DataScheme>(fileContent);
    }

    public DataScheme GetFromType<T>()
        => _mapper.Value.MapFrom<T>();

    public string GenerateJson(DataScheme scheme, bool useLast = false)
        => GenerateJson(new DataProcessor(scheme, _generatorFactory), scheme.Root, useLast);

    public string GenerateJsonCollection(DataScheme scheme, int count, bool useLast = false)
        => GenerateJsonCollection(new DataProcessor(scheme, _generatorFactory), count, scheme.Root, useLast);
        
    public string GenerateJson<T>(DgProfileBase profile, bool useLast = false)
        => GenerateJson(new TypeDataProcessor<T>(profile, _generatorFactory), typeof(T).Name, useLast);
        
    public string GenerateJsonCollection<T>(DgProfileBase profile, int count, bool useLast = false)
        => GenerateJsonCollection(new TypeDataProcessor<T>(profile, _generatorFactory), count, typeof(T).Name, useLast);

    public T GenerateObject<T>(DataScheme scheme, bool useLast = false)
        => JsonConvert.DeserializeObject<T>(GenerateJson(scheme, useLast));
        
    public IEnumerable<T> GenerateObjectCollection<T>(DataScheme scheme, int count, bool useLast = false)
        => JsonConvert.DeserializeObject<IEnumerable<T>>(GenerateJsonCollection(scheme, count, useLast));

    public T GenerateObject<T>(DgProfileBase profile, bool useLast = false)
        => JsonConvert.DeserializeObject<T>(GenerateJson<T>(profile, useLast));
        
    public IEnumerable<T> GenerateObjectCollection<T>(DgProfileBase profile, int count, bool useLast = false)
        => JsonConvert.DeserializeObject<IEnumerable<T>>(GenerateJsonCollection<T>(profile, count, useLast));

    public void SaveToFile(string filename, string json)
        => _ioHelper.Value.SaveData(filename, json);
        
    private string GenerateJson(IDataProcessor dataProcessor, string? scheme, bool useLast)
    {
        ValidateUseLast(useLast);

        string key = scheme ?? "default";

        if (useLast)
            return _runBehavior.ReadLast(key);
        
        var data = dataProcessor.CreateData();

        var result = JsonValueObjectSerializer.Serialize(data);
        
        if (_runBehavior is not NullRunBehavior)
            _runBehavior.SaveResult(key, result);
        
        return result;
    }
        
    private string GenerateJsonCollection(IDataProcessor dataProcessor, int count, string? scheme, bool useLast)
    {
        ValidateUseLast(useLast);

        string key = (scheme ?? "default") + "_collection";
        
        if (useLast)
            return _runBehavior.ReadLast(key);
        
        var dataList = Enumerable.Range(1, count)
            .Select(x =>
            {
                var data = dataProcessor.CreateData();
                return JsonValueObjectSerializer.Serialize(data);
            });
            
        var result = $"[{string.Join(",", dataList)}]";
        
        if (_runBehavior is not NullRunBehavior)
            _runBehavior.SaveResult(key, result);
        
        return result;
    }

    private void ValidateUseLast(bool useLast)
    {
        if (useLast && _runBehavior is NullRunBehavior)
            throw new ArgumentException($"{nameof(useLast)} flag cannot be used with {nameof(NullRunBehavior)}");
    }
}