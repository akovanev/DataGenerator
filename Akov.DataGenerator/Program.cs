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
            var dataBuilder = new JsonDataBuilder();

            try
            {
                DataScheme scheme = ioHelper.GetScheme(filename);
                
                string data = dataBuilder.Build(scheme);
                
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
