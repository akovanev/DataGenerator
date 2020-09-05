using System;
using System.Linq;
using Akov.DataGenerator.Attributes;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Processors;
using Akov.DataGenerator.Serializers;

namespace Akov.DataGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args is null || !args.Any())
            {
                Console.WriteLine("Please input name of the data file");
                return;
            }

            var mapper = new DataSchemeMapper();
            var scheme = mapper.MapFrom<Root>();
            var dataProcessor = new DataProcessor(scheme, new ExtendedGeneratorFactory());

            var data = dataProcessor.CreateData();

            var dataAsJson = JsonValueObjectSerializer.Serialize(data);
            var ioHelper = new IOHelper();
            ioHelper.SaveData("data.json.out.json", dataAsJson);

            //var dg = new DG(new ExtendedGeneratorFactory());

            //Console.WriteLine(dg.Execute(args[0]));
        }
    }
}
