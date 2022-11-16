using System;
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
            => _ioHelper.Value.GetScheme(filename);

        public DataScheme GetFromType<T>()
            => _mapper.Value.MapFrom<T>();

        public string GenerateJson(DataScheme scheme)
        {
            var dataProcessor = new DataProcessor(scheme, _generatorFactory);

            var data = dataProcessor.CreateData();
        
            return JsonValueObjectSerializer.Serialize(data);
        }

        public T GenerateObject<T>(DataScheme scheme)
            => JsonConvert.DeserializeObject<T>(GenerateJson(scheme));

        public T GenerateObject<T>(DgProfileBase profile)
            => GenerateObject<T>(profile.GetDataScheme<T>());

        public void SaveToFile(string filename, string json)
            => _ioHelper.Value.SaveData(filename, json);
    }
}
