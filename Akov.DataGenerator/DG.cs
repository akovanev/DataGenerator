using System;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Processors;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Serializers;

namespace Akov.DataGenerator
{
    public class DG
    {
        private readonly IGeneratorFactory _generatorFactory;

        public DG(IGeneratorFactory? generatorFactory = null)
        {
            _generatorFactory = generatorFactory ?? new GeneratorFactory();
        }

        public string Execute(string inputFile, string? outputFile = null)
        {
            var ioHelper = new IOHelper();
            outputFile ??= $"{inputFile}.out.json";

            try
            {
                DataScheme scheme = ioHelper.GetScheme(inputFile);

                var dataProcessor = new DataProcessor(scheme, _generatorFactory);

                var data = dataProcessor.CreateData();

                var dataAsJson = JsonValueObjectSerializer.Serialize(data);

                ioHelper.SaveData(outputFile, dataAsJson);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
