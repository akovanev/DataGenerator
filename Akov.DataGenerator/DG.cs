using System;
using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.Processors;
using Akov.DataGenerator.Profiles;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Serializers;
using Newtonsoft.Json;

namespace Akov.DataGenerator
{
    public class DG
    {
        private readonly IGeneratorFactory _generatorFactory;
        private readonly Lazy<IOHelper> _ioHelper;
        private readonly Lazy<DataSchemeMapper> _mapper;

        public DG(IGeneratorFactory? generatorFactory = null,
            DataSchemeMapperConfig? mapperConfig = null,
            FileReadConfig? fileReadConfig = null)
        {
            _generatorFactory = generatorFactory ?? new GeneratorFactory();
            _ioHelper = new Lazy<IOHelper>(() => new IOHelper(fileReadConfig));
            _mapper = new Lazy<DataSchemeMapper>(() => new DataSchemeMapper(mapperConfig));
        }

        public DataScheme GetFromFile(string filename)
        {
            string fileContent = _ioHelper.Value.GetFileContent(filename);
            return JsonConvert.DeserializeObject<DataScheme>(fileContent);
        }

        public DataScheme GetFromType<T>()
            => _mapper.Value.MapFrom<T>();

        public string GenerateJson<T>(DgProfileBase profile)
            => GenerateJson(new TypeDataProcessor<T>(profile, _generatorFactory));
        
        public string GenerateJsonCollection<T>(DgProfileBase profile, int count)
            => GenerateJsonCollection(new TypeDataProcessor<T>(profile, _generatorFactory), count);

        public string GenerateJson(DataScheme scheme)
            => GenerateJson(new DataProcessor(scheme, _generatorFactory));

        public string GenerateJsonCollection(DataScheme scheme, int count)
            => GenerateJsonCollection(new DataProcessor(scheme, _generatorFactory), count);

        public T GenerateObject<T>(DataScheme scheme)
            => JsonConvert.DeserializeObject<T>(GenerateJson(scheme));
        
        public IEnumerable<T> GenerateObjectCollection<T>(DataScheme scheme, int count)
            => JsonConvert.DeserializeObject<IEnumerable<T>>(GenerateJsonCollection(scheme, count));

        public T GenerateObject<T>(DgProfileBase profile)
            => JsonConvert.DeserializeObject<T>(GenerateJson<T>(profile));
        
        public IEnumerable<T> GenerateObjectCollection<T>(DgProfileBase profile, int count)
            => JsonConvert.DeserializeObject<IEnumerable<T>>(GenerateJsonCollection<T>(profile, count));

        public void SaveToFile(string filename, string json)
            => _ioHelper.Value.SaveData(filename, json);
        
        private static string GenerateJson(IDataProcessor dataProcessor)
        {
            var data = dataProcessor.CreateData();
            return JsonValueObjectSerializer.Serialize(data);
        }
        
        private static string GenerateJsonCollection(IDataProcessor dataProcessor, int count)
        {
            var dataList = Enumerable.Range(1, count)
                .Select(x =>
                {
                    var data = dataProcessor.CreateData();
                    return JsonValueObjectSerializer.Serialize(data);
                });
            
            return $"[{string.Join(",", dataList)}]";
        }
    }
}
