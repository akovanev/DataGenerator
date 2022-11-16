using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Akov.DataGenerator.Attributes;
using Akov.DataGenerator.Constants;
using Akov.DataGenerator.Extensions;
using Akov.DataGenerator.Scheme;

namespace Akov.DataGenerator.Mappers
{
    public class DataSchemeMapper
    {
        private readonly DataSchemeMapperConfig? _config;

        public DataSchemeMapper(DataSchemeMapperConfig? config = null)
        {
            _config = config;
        }

        public DataScheme MapFrom<T>()
        {
            Type type = typeof(T);

            var propsDictionary = new Dictionary<Type, PropertyInfo[]>();
            type.AddToPropsDictionary(propsDictionary);

            var definitions = new List<Definition>();
            foreach (var (key, value) in propsDictionary)
            {
                definitions.Add(new Definition(key.Name, PopulateProperties(value)));
            }

            return new DataScheme(type.Name, definitions);
        }

        private List<Property> PopulateProperties(PropertyInfo[] props)
        {
            var properties = new List<Property>();

            foreach (PropertyInfo prop in props)
            {
                var attrs = prop.GetCustomAttributes().ToList();

                var ignore = attrs.GetValue<DgIgnoreAttribute>();
                if(ignore is not null) continue;

                var name = attrs.GetValue<DgNameAttribute>();
                var propertyName = name?.Value ?? prop.Name;
                if (_config is not null && _config.UseCamelCase)
                    propertyName = propertyName.ToCamelCase();

                var calc = attrs.GetValue<DgCalcAttribute>();
                if (calc is not null)
                {
                    properties.Add(new Property {Name = propertyName, Type = TemplateType.Calc});
                    continue;
                }

                var source = attrs.GetValue<DgSourceAttribute>();

                string? templateType = prop.PropertyType.GetPropertyTemplateType();

                string? pattern = attrs.GetValue<DgPatternAttribute>()?.Value;
                DgLengthAttribute? length = null;
                DgSpacesCountAttribute? spaces = null;
                DgRangeAttribute? range = null;

                if (source is not null)
                {
                    if(templateType == TemplateType.Array || 
                     templateType == TemplateType.Object ||
                     prop.PropertyType.IsEnum)
                        throw new NotSupportedException($"{nameof(DgSourceAttribute)} " +
                                                        $"may be applied only to primitive types except enums");
                    
                    templateType = TemplateType.File;
                    pattern = source.Path;
                }
                else
                {
                    switch (templateType)
                    {
                        case TemplateType.Set:
                            pattern = string.Join(",", Enum.GetNames(prop.PropertyType));
                            break;
                        case TemplateType.Array:
                        {
                            pattern = prop.PropertyType.GetArrayPatternTemplateType();
                            break;
                        }
                        case TemplateType.Object:
                            pattern = prop.PropertyType.Name;
                            break;
                    }

                    length = attrs.GetValue<DgLengthAttribute>();
                    spaces = attrs.GetValue<DgSpacesCountAttribute>();
                    range = attrs.GetValue<DgRangeAttribute>();
                }

                var failure = attrs.GetValue<DgFailureAttribute>();
                var customFailure = attrs.GetValue<DgCustomFailureAttribute>();
                var separator = attrs.GetValue<DgSequenceSeparatorAttribute>();
                var subpattern = attrs.GetValue<DgSubTypePatternAttribute>();

                var property = new Property
                {
                    Name = propertyName,
                    Type = templateType,
                    Pattern = pattern,
                    SubTypePattern = subpattern?.Value,
                    MinLength = length?.Min,
                    MaxLength = length?.Max,
                    MinSpaceCount = spaces?.Min,
                    MaxSpaceCount = spaces?.Max,
                    MinValue = range?.Min,
                    MaxValue = range?.Max,
                    Failure = failure != null
                        ? new Failure
                        {
                            Nullable = failure.NullProbability > 0 ? failure.NullProbability : (double?)null,
                            Custom = failure.CustomProbability > 0 ? failure.CustomProbability : (double?)null,
                            Range = failure.OutOfRangeProbability > 0 ? failure.OutOfRangeProbability : (double?)null
                        }
                        : null,
                    CustomFailure = customFailure?.Value,
                    SequenceSeparator = separator?.Value
                };

                properties.Add(property);
            }

            return properties;
        }
    }
}
