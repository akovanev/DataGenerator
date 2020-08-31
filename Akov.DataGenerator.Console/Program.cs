using System;
using System.Linq;
using Akov.DataGenerator.Generators;

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

            var dg = new DG(new ExtendedGeneratorFactory());

            Console.WriteLine(dg.Execute(args[0]));
        }
    }
}
