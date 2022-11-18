using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Processors
{
    public interface IDataProcessor
    {
        NameValueObject CreateData();
    }
}