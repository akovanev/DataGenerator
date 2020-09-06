using System.Text;
using Akov.DataGenerator.Attributes;

namespace Akov.DataGenerator.Demo.StudentsSample.Responses
{
    public class Result
    {
        [DgIgnore]
        public StringBuilder ParsingErrors { get; set; } = new StringBuilder();
        
        [DgIgnore]
        public bool IsValid => ParsingErrors.Length == 0;
    }
}
