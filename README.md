# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/) [![](https://img.shields.io/nuget/dt/akov.datagenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

 Data Generator. Give it a &#11088; if you find it useful.
 <hr/>

# ${\color{grey}{Key \space features}}$ 
## ${\color{darkgreen}{Calculated \space properties}}$  
```csharp
// Given: FirstName and LastName are randomly generated.
// The email is dynamically constructed based on their values using the following template:
.Property(s => s.Email).Construct(s => $"{s.FirstName}.{s.LastName}@mycompany.com") 
```

## ${\color{darkgreen}{String \space templates}}$  

Templates support placeholders for number ranges, [resources](https://github.com/akovanev/DataGenerator/tree/master/DataGenerator/Resources), and file content. For example, a template like:

`"Note: [number:100-999] [resource:Firstnames] [file:lastnames.txt]"`
Could generate an output such as:

`"Note: 137 Jessica Torres"`.

## ${\color{blue}{Decorators}}$

Enhance the freshly generated random value using decorators. 

```
.Property(s => s.Name)
    .Template("[resource:Nouns]")
    .Decorate(r => CapitalizeFirstLetter(r?.ToString()))
```

## ${\color{darkgreen}{Random \space generation \space rules}}$ 
Defines rules for generating values, each associated with a probability.

The probability of the main generation flow (P<sub>m</sub>) is calculated as: P<sub>m</sub> = 1 - ΣP<sub>i</sub>

Where P<sub>i</sub> represents the probability of each individual generation rule.

```csharp
// Generates a negative value in the range of [-5, -1) with a probability of 0.1 (10%).
.Property(s => s.Year)
    .Range(1,5)
    .GenerationRule("NegativeYear", 0.1, _ => Random.Shared.Next(-5, -1))
```

## ${\color{darkgreen}{Nullable \space rule}}$  

Defines the probability for the null value. The property type should either be nullable or have nullable reference type enabled.

```csharp
// Precondition
class Student
{
    public string? FirstName {get; set; }
    public string LastName {get; set; }
}

// Great job :)
.Property(s => s.FirstName)
    .Template("[file:firstnames.txt]")
    .Nullable(0.25)

// Will throw an exception :(
.Property(s => s.FirstName)
    .Template("[file:lastnames.txt]")
    .Nullable(0.25)
```

## ${\color{darkgreen}{Custom \space generators}}$  


```csharp
// A custom generator should inherit from `GeneratorBase<T>`
public class PhoneGenerator : GeneratorBase<string>
{
    private const char SpecialSymbol = '#';

    public override string CreateRandomValue(Property property)
    {
        var mask = property.GetValueRule("PhoneMask") ?? "### ### ###";

        var builder = new StringBuilder();
        Random random = GetRandomInstance(property);

        foreach (var c in mask.ToString()!)
            builder.Append(c != SpecialSymbol ? c : random.Next(0, 10).ToString());

        return builder.ToString();
    }
}

// Usage
.Property(s => s.Phone)
    .UseGenerator(new PhoneGenerator())
    .ValueRule("PhoneMask", "## ## ## ##")
```

## ${\color{black}{Generators}}$  


* `BooleanGenerator`
* `DatetimeGenerator`
* `DecimalGenerator`
* `DoubleGenerator`
* `EmailGenerator`
* `EnumGenerator`
* `GuidGenerator`
* `IntGenerator`
* `IpGenerator`
* `PhoneGenerator`
* `StringGenerator`


## ${\color{black}{Example}}$ ➫
```csharp
var scheme = new DataScheme();

//StudentGroup
scheme.ForType<StudentGroup>()
    .Property(s => s.Name).Template("[resource:Nouns]").Decorate(r => Utility.CapitalizeFirstLetter(r?.ToString()))
    .Property(s => s.Students).Count(3, 5);

//Student
scheme.ForType<Student>()
    .Property(s => s.FirstName).Template("[resource:Firstnames]")
    .Property(s => s.LastName).Template("[file:lastnames.txt]").Nullable(0.25)
    .Property(s => s.Email).Construct(s => $"{Utility.BuildNameWithoutSpaces(s.FirstName, s.LastName)}" + 
                                           $"{TemplateProcessor.CreateValue(Random.Shared,"@[resource:Domains]")}")
    .Property(s => s.BirthDay).Range(DateTime.Today.AddYears(-60), DateTime.Today.AddYears(-16)).Nullable(0.1)
    .Property(s => s.Year).Range(1,5).GenerationRule("NegativeYear", 0.5, _ => Random.Shared.Next(-5, -1))
    .Property(s => s.Courses).Count(2,4)
    .Property(s => s.Note).Template("Note: [number:100-999] [resource:Firstnames] [file:lastnames.txt]");

//Contact
scheme.ForType<Contact>()
    .Property(s => s.Phone).UseGenerator(new PhoneGenerator()).PhoneMask("## ## ## ##")
    .Property(s => s.Address).Template("[resource:Addresses]")
    .Property(s => s.IpAddress).UseGenerator(new IpGenerator()).Nullable(0.5);

var dataGenerator = new SimpleDataGenerator(scheme);
var randomStudentCollection = dataGenerator.GenerateForType<StudentGroup>();
```

