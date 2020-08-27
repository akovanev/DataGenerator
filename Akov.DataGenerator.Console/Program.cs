using Akov.DataGenerator.IO;
using System;
using System.Linq;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Processors;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Serializers;
using Newtonsoft.Json;

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

            Console.WriteLine(Run(args[0]));
        }

        internal static string Run(string filename)
        {
            var ioHelper = new IOHelper();
            var generatorFactory = new GeneratorFactory();

            try
            {
                DataScheme scheme = ioHelper.GetScheme(filename);
                var dataProcessor = new DataProcessor(scheme, generatorFactory);
                
                var data = dataProcessor.CreateData();

                var dataAsJson = JsonValueObjectSerializer.Serialize(data);

                ioHelper.SaveData($"{filename}.out.json", dataAsJson);

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
