using System.IO;
using Akov.DataGenerator.Scheme;
using Newtonsoft.Json;

namespace Akov.DataGenerator.Common
{ 
    /// <summary>
    /// The wrapper on the StreamReader and StreamWriter.
    /// </summary>
    public class IOHelper
    {
        public DataScheme GetScheme(string filename)
        {
            string input = GetFileContent(filename);
            return JsonConvert.DeserializeObject<DataScheme>(input);
        }

        public string GetFileContent(string filename)
        {
            using var reader = new StreamReader(filename);
            return reader.ReadToEnd();
        }

        public void SaveData(string filename, string data)
        {
            using var writer = new StreamWriter(filename);
            writer.WriteLine(data);
        }
    }
}
