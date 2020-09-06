using System;
using System.Linq;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args is null || !args.Any())
            {
                Console.WriteLine("Parameter for input json file is missing");
                return;
            }

            try
            {
                var dg = new DG(new ExtendedGeneratorFactory());
                DataScheme scheme = dg.GetFromFile(args[0]);
                string jsonData = dg.GenerateJson(scheme);
                dg.SaveToFile($"{args[0]}.out.json", jsonData);
                
                Console.WriteLine("Success");
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message);
            }
        }
    }
}
