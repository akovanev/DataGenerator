using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Serializers;
using Newtonsoft.Json;

namespace Akov.DataGenerator.Models;

public class CalcPropertyObject(string definitionName, Property property, IEnumerable<NameValueObject> values)
    : PropertyObject(definitionName, property)
{
    public List<NameValueObject> Values { get; } = values.ToList();

    public T Cast<T>()
    {
        var rootObject = new NameValueObject("", Values);
        var json = JsonValueObjectSerializer.Serialize(rootObject);
        return JsonConvert.DeserializeObject<T>(json);
    }
}