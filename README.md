# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/) [![](https://img.shields.io/nuget/dt/akov.datagenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

 Data Generator. Give it a &#11088; if you find it useful.

 [ChangeLog](https://github.com/akovanev/DataGenerator/wiki#changelog)
 <hr/>

# ${\color{grey}{Key \space features}}$ 
## ${\color{darkgreen}{Calculated \space properties}}$  
Construct a property value using just generated values from the same object.

```csharp
// Given: FirstName and LastName are randomly generated. The email is dynamically constructed. 
.Property(s => s.Email).Construct(s => $"{s.FirstName}.{s.LastName}@mycompany.com") 
```

## ${\color{darkgreen}{String \space templates}}$  

Support placeholders for numbers, oneofs, [resources](https://github.com/akovanev/DataGenerator/tree/master/DataGenerator/Resources), and file content, replacing them with values from the corresponding list. 

A template like:

```csharp
"Note: [number:100-999] [resource:Firstnames] [file:lastnames.txt:4-10] [oneof:Europe,America,Africa]"
```
could generate an output such as: `Note: 137 Jessica Torres Africa`.

> [!Note]
> Specifying a range for numbers is required.

> [!Note]
> The built-in [`Length(min, max)`](https://github.com/akovanev/DataGenerator/blob/e0843da79550110324b829d6ea437946746c7692/DataGenerator/FluentSyntax/PropertyBuilderExtensions.cs#L50) method is ignored when using templates.

## ${\color{blue}{Decorators}}$

Enhance the just generated random value using decorators. 

```
.Property(s => s.Name)
    .Template("[resource:Nouns]")
    .Decorate(r => CapitalizeFirstLetter(r?.ToString()))
```

## ${\color{darkgreen}{Random \space generation \space rules}}$ 
Define rules for generating values, each associated with a probability.

```csharp
// Generates a negative value in the range of [-5, -1) with a probability of 0.1 (10%).
.Property(s => s.Year)
    .Range(1,5)
    .GenerationRule("NegativeYear", 0.1, _ => Random.Shared.Next(-5, -1))
```

> [!Note]
> The probability of using the default generation logic (P<sub>d</sub>) — when no specific generation rules apply — is calculated as: P<sub>d</sub> = 1 - ΣP<sub>i</sub>.
Where P<sub>i</sub> represents the probability of each individual generation rule.

## ${\color{darkgreen}{Nullable \space rule}}$  

Defines the probability of assigning a null value. 

> [!Note]
> The property type must be either nullable (`T?`) or a reference type with nullable annotations enabled (`?`).
> 
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

## ${\color{darkgreen}{Predefined \space values}}$
Choose a value from the predefined collection.
```
.Property(s => s.Degree)
    .FromCollection(["Bachelor", "Master", "Doctor"])
```

## ${\color{darkgreen}{Ignore}}$
Excludes the property from the generation process.
```csharp
scheme.ForType<StudentGroup>()
    .Ignore(s => s.CourseTeachers)
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
* `DateTimeGenerator`
* `DateTimeOffsetGenerator`
* `DecimalGenerator`
* `DoubleGenerator`
* `EmailGenerator`
* `EnumGenerator`
* `GuidGenerator`
* `IntGenerator`
* `IpGenerator`
* `PhoneGenerator`
* `SetGenerator`
* `StringGenerator`


## ${\color{black}{Example}}$ ➫
```csharp
var scheme = new DataScheme();

//StudentGroup
scheme.ForType<StudentGroup>()
    .Ignore(s => s.CourseTeachers)
    .Property(s => s.Name).Template("[resource:Nouns]").Decorate(r => Utility.CapitalizeFirstLetter(r?.ToString()))
    .Property(s => s.Students).Count(3, 5)
    .Property(s => s.ContactPhones).Count(2, 3).UseGenerator(new PhoneGenerator());

//Student
scheme.ForType<Student>()
    .Property(s => s.FirstName).Template("[resource:Firstnames:3-4]")
    .Property(s => s.LastName).Template("[file:lastnames.txt]:4-6").Nullable(0.25)
    .Property(s => s.Email).Construct(s => $"{Utility.BuildNameWithoutSpaces(s.FirstName, s.LastName)}" + 
                                           $"{TemplateProcessor.CreateValue(Random.Shared,"@[resource:Domains]")}")
    .Property(s => s.BirthDay).Range(DateTime.Today.AddYears(-60), DateTime.Today.AddYears(-16)).Nullable(0.1)
    .Property(s => s.Year).Range(1,5).GenerationRule("NegativeYear", 0.5, _ => Random.Shared.Next(-5, -1))
    .Property(s => s.Courses).Count(2,4)
    .Property(s => s.Degree).FromCollection(["Bachelor", "Master", "Doctor"])
    .Property(s => s.Note).Template("Note: [number:100-999] [resource:Firstnames] [file:lastnames.txt] [oneof:Europe,America,Africa].");

//Contact
scheme.ForType<Contact>()
    .Property(s => s.Phone).UseGenerator(new PhoneGenerator()).PhoneMask("## ## ## ##")
    .Property(s => s.Address).Template("[resource:Addresses]")
    .Property(s => s.IpAddress).UseGenerator(new IpGenerator()).Nullable(0.5);

var dataGenerator = new SimpleDataGenerator(scheme);
var randomStudentCollection = dataGenerator.GenerateForType<StudentGroup>();
```
## ${\color{black}{Default \space values}}$
The range [default values](https://github.com/akovanev/DataGenerator/blob/master/DataGenerator/Core/Constants/DefaultValues.cs) can be overriden if necessary.
