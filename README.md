# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/) [![](https://img.shields.io/nuget/dt/akov.datagenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

Generates data randomly. Give it a &#11088; if you find it useful.

**Key features**:
* Calculated properties.
```csharp
.Property(c => c.Count).IsCalc() // Custom calc generator will define the logic rules
```
* Assigned properties.
```csharp
.Property(s => s.FullName).Assign(s => $"{s.FirstName} {s.LastName}") // FirstName and LastName are random here
```
* Random failures.
```csharp
.Property(s => s.Signature).Length(4, 16)
    .Failure(
        nullable: 0.1, // 10% for null value
        custom: 0.1,   // 10% for bad value
        range: 0.05);  // 5% for out of range. Here that means lenght < 4 or > 16 
```

* Predefined list of firstnames, lastnames, companies, addresses, cities, countries.
```csharp
.Property(s => s.Company).FromResource(ResourceType.Companies)
```

**Links**
* [`Project documentation`](https://github.com/akovanev/DataGenerator/wiki)
* [Integration tests demo](https://github.com/akovanev/DataGenerator/blob/master/Akov.DataGenerator.Demo/StudentsSampleTests/Tests/StudentHttpServiceTests.cs)

**History**

Originally this library was created in order to prepare json mocked data for testing api, which was under development. Api was supposed to aggregate data from a bunch of backend systems, while the client had to import several hundred thousand objects to a new frontend cms. It was extremely important to make sure that all data format surprises, which we were getting, could have been handled properly. That's why **random failures** were added to `DG`. As well as random sized arrays. At the same time, it was impossible to import an object if all properties contained completely random data. This is where **calculated properties** hepled. 

After that I didn't work with `DG` for almost two years. During this time a couple of nice tools, helping out with fake data, appeared. Some time ago, I was working on the other issue, where data couldn't be too random again. I realized, that `DG` may get its second chance and got back to updating it with new features, primarily focusing on building a more convenient setup, based on the fluent syntax.

`DG`, at this moment, is quite far from being optimal in terms of performance as well as in code experience, test coverage or documentation. But if you see that there could be nice features added, feel free to create a github [issue](https://github.com/akovanev/DataGenerator/issues) or [discussion](https://github.com/akovanev/DataGenerator/discussions). 

**Generators**
* `BooleanGenerator`
* `ByteArrayGenerator`
* `DatetimeGenerator`
* `DecimalGenerator`
* `DoubleGenerator`
* `GuidGenerator`
* `IntGenerator`
* `PhoneGenerator`
* `SetGenerator` _list of values and enums_
* `StringGenerator`

**Special generators**
* `AssignGenerator`
* `CalcGeneratorBase`

**Example**
```csharp
// In StudentsTestProfile profile constructor
ForType<Student>()
    .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
    .Property(s => s.Id).Failure(nullable: 0.2)
    .Property(s => s.FirstName).FromFile("firstnames.txt").Failure(nullable: 0.1)
    .Property(s => s.LastName).FromResource(ResourceType.LastNames).Failure(nullable: 0.1)
    .Property(s => s.FullName).Assign(s => $"{s.FirstName} {s.LastName}")
    .Property(s => s.Year).UseGenerator("MyUint").Range(5)
    .Property(s => s.Variant).HasJsonName("test_variant")
    .Property(s => s.TestAnswers).HasJsonName("test_answers").Length(5).Range(1, 5)
    .Property(s => s.EncodedSolution).HasJsonName("encoded_solution")
        .Pattern(StringGenerator.AbcNum).Length(15, 50).Spaces(1,3)
        .Failure(0.1, 0.1, 0.05, "####-####-####" )
    .Property(s => s.LastUpdated).HasJsonName("last_updated").Pattern("dd/MM/yy")
        .Range("20/10/19","01/01/20").Failure(0.2, 0.2, 0.1)
    .Property(s => s.Subjects).Length(4)
    .Property(s => s.Discount).Pattern("##.##").Range(9.50, 99.50)
    .Property(s => s.Signature).Length(4, 16).Failure(nullable: 0.1);

ForType<Address>()
    .Property(s => s.Company).FromResource(ResourceType.Companies).Failure(nullable: 0.1)
    .Property(s => s.Phone).UseGenerator(TemplateType.Phone)
        .Pattern("+45 ## ## ## ##;+420 ### ### ###")
        .Failure(nullable: 0.05)
    .Property(s => s.AddressLine).FromResource(ResourceType.Addresses).Failure(nullable: 0.25)
    .Property(s => s.City).FromResource(ResourceType.Cities).Failure(nullable: 0.1)
    .Property(s => s.Country).FromResource(ResourceType.Countries).Failure(nullable: 0.15);

// Execute 
var dg = new DG(new StudentGeneratorFactory(), new DataSchemeMapperConfig { UseCamelCase = true }); 

// Json
string jsonData = dg.GenerateJson<StudentCollection>(new StudentsTestProfile());
//or objects
IEnumerable<Student> students = dg.GenerateObjectCollection<Student>(new StudentsTestProfile(), 100); 

```

