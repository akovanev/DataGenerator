using System;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Mappers;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator
{
    class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var dg = new DG(
                    new ExtendedGeneratorFactory(),
                    new DataSchemeMapperConfig {UseCamelCase = true});

                DataScheme scheme1 = dg.GetFromFile("data.json");
                string jsonData1 = dg.GenerateJson(scheme1);
                dg.SaveToFile("data.out.json", jsonData1);

                DataScheme scheme2 = dg.GetFromType<RootDto>();
                string jsonData2 = dg.GenerateJson(scheme2);
                dg.SaveToFile("data2.out.json", jsonData1);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
