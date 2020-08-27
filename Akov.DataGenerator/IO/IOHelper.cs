using System.IO;
using Akov.DataGenerator.Scheme;
using Newtonsoft.Json;

namespace Akov.DataGenerator.IO
{ 
    public class IOHelper
    {
        public DataScheme GetScheme(string filename)
        {
            using var reader = new StreamReader(filename);
            string input = reader.ReadToEnd();
            return JsonConvert.DeserializeObject<DataScheme>(input);
        }

        public void SaveData(string filename, string data)
        {
            using var writer = new StreamWriter(filename);
            writer.WriteLine(data);
        }
    }
}
