using Akov.DataGenerator.Common;

namespace Akov.DataGenerator.Processors
{
    public interface IDataPcocessor
    {
        ValueObject CreateData();
    }
}