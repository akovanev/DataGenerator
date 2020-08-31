using System.Collections.Generic;
using Akov.DataGenerator.Common;
using Akov.DataGenerator.Models;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Factories
{
    internal class PropertyObjectFactory
    {
        private readonly IOHelper _ioHelper = new IOHelper();

        public PropertyObject CreatePropertyObject(
            string definitionName, 
            Property property)
        {
            if (property.Type == TemplateType.File)
            {
                property.Pattern.ThrowIfNull($"Property {property.Name} does not have a pattern");
                string fileContent = _ioHelper.GetFileContent(property.Pattern!);
                return new PropertyObject(definitionName, property, fileContent);
            }

            return new PropertyObject(definitionName, property);
        }

        public CalcPropertyObject CreateCalcPropertyObject(
            string definitionName,
            Property property,
            List<NameValueObject> values)
        {
            values.ThrowIfNullOrEmpty($"The object owning the calculated property " +
                                      $"{property.Name} should have at least one not calculated one");
            
            return new CalcPropertyObject(definitionName, property, values);
        }
    }
}
