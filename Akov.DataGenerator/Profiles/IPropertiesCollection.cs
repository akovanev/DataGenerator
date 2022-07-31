using System.Collections.Generic;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Profiles;

public interface IPropertiesCollection
{
    List<Property> Properties { get; }
}