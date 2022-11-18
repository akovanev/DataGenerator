# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/) [![](https://img.shields.io/nuget/dt/akov.datagenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

Generates data randomly based on 1) json file, 2) class attributes, 3) fluent profiles (since version 1.4). 

Key features:
* Calculated properties with custom rules.
* Random failures.

**History**:

Originally this library was created in order to prepare json mocked data for testing api, which was under development. Api was supposed to aggregate data from a bunch of backend systems, while the client had to import several hundred thousand objects to a new frontend cms. It was extremely important to make sure that all data format surprises, which we were getting, could have been handled properly. That's why **random failures** were added to `DG`. As well as random sized arrays. At the same time, it was impossible to import an object if all properties contained completely random data. This is where **calculated properties** hepled. 

After that I didn't work with `DG` for almost two years. During this time a couple of nice tools, helping out with fake data, appeared. Some time ago, I was working on the other issue, where data couldn't be too random again. I realized, that `DG` may get its second chance and got back to updating it with new features, primarily focusing on building a more convenient setup, based on the fluent syntax.

`DG`, at this moment, is quite far from being optimal in terms of performance as well as in code experience, test coverage or documentation. But if you see that there could be nice features added, feel free to create a github issue.  


**Changelog**: 
* Versions **1.0 - 1.2** are not supported. 
* Version **1.3** mapping of type `T` based on attributes from `Akov.DataGenerator.Attributes` added. 
* Version **1.3.1** min array size support added, range validation for `DgLength`, `DgSpacesCount`, `DgFailure` added on attributes level.
* Version **1.4.0** fluent support added.
```csharp
ForType<Student>()
    .Ignore(s => s.IsValid)
    .Property(s => s.Id).Failure(nullable: 0.2)
    .Property(s => s.FirstName).FromFile("firstnames.txt").Failure(nullable: 0.1)
    .Property(s => s.LastName).FromFile("lastnames.txt").Failure(nullable: 0.1)
    .Property(s => s.FullName).IsCalc()
    .Property(s => s.TestAnswers).HasJsonName("test_answers").Length(5).Range(1, 5)
    .Property(s => s.EncodedSolution).HasJsonName("encoded_solution")
        .Pattern("abcdefghijklmnopqrstuvwxyz0123456789").Length(15, 50).Spaces(1,3)
        .Failure(0.1, 0.1, 0.05, "####-####-####" )
    .Property(s => s.LastUpdated).HasJsonName("last_updated").Pattern("dd/MM/yy")
        .Range("20/10/19","01/01/20").Failure(0.2, 0.2, 0.1)
```

* Version **1.4.1** object generation interface added.
```csharp
var students = dg.GenerateObject<StudentCollection>(new StudentsTestProfile());
```

* Version **1.5.0** assigning of calculated properties added to fluent support.
```csharp
ForType<Student>()
    .Property(s => s.FullName).Assign(s => $"{s.FirstName} {s.LastName}")
```

## Author's blog

Articles temporarily available on https://github.com/akovanev/kovanev.github.io/tree/master/_posts

[Data Generator](https://akovanev.com/blogs/2020/08/26/data-generator/) - a quick introduction.

[Custom Data Generator](https://akovanev.com/blogs/2020/08/27/custom-data-generator/) - creating a custom generator based on the library.

[Calculated Properties with Data Generator](https://akovanev.com/blogs/2020/08/31/calculated-properties-with-data-generator/) - calculated properties and files as custom data sources.

[Data Generator Attributes](https://akovanev.com/blogs/2020/09/07/data-generator-attributes) - using the program approach.

[Integration Testing with Data Generator](https://akovanev.com/blogs/2020/09/08/integration-testing-with-data-generator) - shows how to mock and test solutions with the generated data.

[Fluent support profile example](https://github.com/akovanev/DataGenerator/blob/feature/1.4/Akov.DataGenerator.Demo/StudentsSampleTests/Tests/Profiles/StudentsTestProfile.cs) - an alternative to using attributes.

## Assemblies description

`Akov.DataGenerator` the library itself. The nuget package is available via the link above.

`Akov.DataGenerator.Demo` demonstrates how to mock an http client with generated data, and then use it in tests.

## DataScheme

`root` points to the entry definition.

`definitions` determines objects. 

### Definition

`name` stands for the object name.

`properties` the object properties.

### Properties

`type` the property type. 

Primitive types:
* `string` the string.
* `set` one of the list of values.
* `guid` the guid.
* `bool` True or False.
* `int` the integer.
* `double`the double.
* `datetime` the datatime.

Complex types:

* `object` the object from definition.
* `array` the array.
* `file` the file of values that will be transformed to the `set` primitive type.
* `calc` the calculated property. Having it means that a custom generator, specifying the property behaviour, and derived from the `CalcGeneratorBase` should be added to the `GeneratorFactory`.
 
`pattern` 
* for `string` defines all possible characters, e.g. "abcdefghABCFEDGH". Spaces will be added additionally.
* for `set` defines all possible values separated by comma by default.
* for `double`, `guid`, `datetime` specifies the output format, e.g. "0.00", "yyyy-MM-dd" etc.
* for `object` points to the definition name.
* for `array` points to the definition name if it represents the object type, and to a primitive type otherwise.
* for `file` specifies the path to an existing file. The data are separated by comma by default. 

`subpattern` is a primitive type pattern used only within arrays of primitive types.
```
"name": "prices",
"type": "array", // <- should be array
"pattern": "double" , // <- the pattern keyword specifies the type of array
"subpattern": "0.00", // <- the pattern for the double primitive type
```

`sequenceSeparator` the separator for the `set` and `file`.

`minLength` the minimum output data length for `string`, the min size for `array`.

`maxLength` the maximum output data length for `string`, the max size for `array`.

`minSpaceCount` the minimum count of spaces in the `string`.

`maxSpaceCount` the maximum count of spaces in the `string`.

`minValue` the minimum value for `int`, `double` and `datetime`.

`maxValue` the maximum value for `int`, `double` and `datetime`.

`failure` means inconsistent data appears with the specified probability.
* `nullable` the probability that *null* appears.
* `custom` the probability that the invalid value appears.
* `range` the probability that the value will be out of range. For `string` that means that the string length will be out of the specified interval.

`customFailure` specifies the value that will appear for the `custom` failure case.

### Example of an input json

[data.json](https://github.com/akovanev/DataGenerator/blob/master/data.json)

## Akov.DataGenerator.Attributes

Determine how data should be generated based on an existing class in code.

`DgCalc` considered the property to be calculated.

`DgCustomFailure` specifies the value that will appear for the `custom` failure case.

`DgFailure` specifies probabilities of the different failure types.

`DgIgnore` ignores the property while data generation.

`DgLength` specifies the length of the string. The `Max` value is also used for the array size.

`DgName` specifies the name for the property while generation. Makes sense if it should be different from the class property name.

`DgPattern` specifies the pattern. Unlike the generation process from a json, there is no need to have an additional subpattern attribute for primitive arrays. As the type of array will be determined by the class property type.

`DgRange` specifies the range of values for the property.

`DgSequenceSeparator` specifies the output separator if the property type is enum, and the input one if the pattern points to a file of values.

`DgSource` specifies the file of values for the property. **Not** supported for objects, arrays and enums.

`DgSpaceCount` specifies the count of spaces in the string.

## Fluent profiles

A profile should derive from `DgProfileBase`.

`ForType()` - registers a type in the profile. All the subtypes should be registered as well.

`Ignore(Expression)` - excludes a property from data generation.

`Property(Expression)` - points to a property for which the generation rules should be setup. If a property is not ignored and skipped in the profile, then the defaults will be applied to it.

[PropertyBuilder](https://github.com/akovanev/DataGenerator/blob/feature/1.4/Akov.DataGenerator/Profiles/PropertyBuilder.cs) - contains the list of methods which work similar to the attribute approach.



## Code examples

#### A custom generator for a calculated property

```csharp
public class StudentCalcGenerator : CalcGeneratorBase
{
    protected override object CreateImpl(CalcPropertyObject propertyObject)
    {
        if (string.Equals(propertyObject.Property.Name, "fullname", StringComparison.OrdinalIgnoreCase))
        {
            var val1 = propertyObject.Values
                .Single(v => String.Equals(v.Name, "firstname", StringComparison.OrdinalIgnoreCase));
            var val2 = propertyObject.Values
                .Single(v => String.Equals(v.Name, "lastname", StringComparison.OrdinalIgnoreCase));
           
            return $"{val1.Value} {val2.Value}";
        }

        throw new NotSupportedException($"Unexpected calculated property {propertyObject.Property.Name}");
    }

    protected override object CreateRangeFailureImpl(CalcPropertyObject propertyObject)
    {
        throw new NotSupportedException($"Range failure not supported for {propertyObject.Property.Name}");
    }
}
```

#### Extending the existing generator factory

```csharp
public class StudentGeneratorFactory : GeneratorFactory
{
    public override Dictionary<string, GeneratorBase> GetGeneratorDictionary()
    {
        Dictionary<string, GeneratorBase> generators = base.GetGeneratorDictionary();
        generators.Add(TemplateType.Calc, new StudentCalcGenerator());
        return generators;
    }
}

var dg = new DG(new StudentGeneratorFactory());
```

#### A class representing the data generation

```csharp
public class DgStudent
{
    [DgFailure(NullProbability = 0.2)]
    public Guid Id { get; set; }

    [DgSource("firstnames.txt")]
    [DgFailure(NullProbability = 0.1)]
    public string? FirstName { get; set; }

    [DgSource("lastnames.txt")]
    [DgFailure(NullProbability = 0.1)]
    public string? LastName { get; set; }

    [DgCalc] //supposed to be calculated
    public string? FullName { get; set; }

    [DgName("test_variant")] //Variant represents enum here
    public Variant Variant { get; set; }

    [DgName("test_answers")]
    [DgRange(Min = 1, Max = 5)]
    [DgLength(Max = 5)]
    public int[]? TestAnswers { get; set; }

    [DgName("encoded_solution")]
    [DgPattern("abcdefghijklmnopqrstuvwxyz0123456789")]
    [DgLength(Min = 15, Max = 50)]
    [DgSpacesCount(Min = 1, Max = 3)]
    [DgFailure(
        NullProbability = 0.1,
        CustomProbability = 0.1,
        OutOfRangeProbability = 0.05)]
    [DgCustomFailure("####-####-####")]
    public string? EncodedSolution { get; set; }

    [DgName("last_updated")]
    [DgPattern("dd/MM/yy")]
    [DgRange(Min = "20/10/19", Max = "01/01/20")]
    [DgFailure(
        NullProbability = 0.2,
        CustomProbability = 0.2,
        OutOfRangeProbability = 0.1)]
    public DateTime? LastUpdated { get; set; }

    //DgSubject represents a similar class
    public DgSubject? Subject { get; set; }

    //Collections are equal to arrays in the data generation process
    public List<DgSubject>? Subjects { get; set; }
}

```
