using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Scheme;
using Akov.DataGenerator.Serializers;
using Newtonsoft.Json;

namespace Akov.DataGenerator.Models;

public class CalcPropertyObject : PropertyObject
{
    public CalcPropertyObject(string definitionName, Property property, IEnumerable<NameValueObject> values) 
        : base(definitionName, property)
    {
        Values = values.ToList();
    }

    public List<NameValueObject> Values { get; }

    public T Cast<T>()
    {
        var rootObject = new NameValueObject("", Values);
        var json = JsonValueObjectSerializer.Serialize(rootObject);
        return JsonConvert.DeserializeObject<T>(json);
    }
}