using System.Collections.Generic;
using System.Linq;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Models
{
    public class CalcPropertyObject : PropertyObject
    {
        public CalcPropertyObject(string definitionName, Property property, IEnumerable<NameValueObject> values) 
            : base(definitionName, property)
        {
            Values = values.ToList();
        }

        public List<NameValueObject> Values { get; }
    }
}