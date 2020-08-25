using System;
using System.Linq;
using Akov.DataGenerator.DataBuilders;
using Akov.DataGenerator.IO;
using Akov.DataGenerator.Processor;
using Akov.DataGenerator.Scheme;

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
                var dataBuilder = new JsonDataBuilder(generatorFactory, scheme);
                
                string data = dataBuilder.Build();
                
                ioHelper.SaveData($"{filename}.out.json", data);
                
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
