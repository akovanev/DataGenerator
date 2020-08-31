using Akov.DataGenerator.Models;

namespace Akov.DataGenerator.Processors
{
    public interface IDataPcocessor
    {
        NameValueObject CreateData();
    }
}