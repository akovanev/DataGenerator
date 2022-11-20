# DataGenerator

[![](https://img.shields.io/nuget/v/Akov.DataGenerator)](https://www.nuget.org/packages/Akov.DataGenerator/) [![](https://img.shields.io/nuget/dt/akov.datagenerator)](https://www.nuget.org/packages/Akov.DataGenerator/)

Generates data randomly based on
* Json file.
* Class attributes (since version 1.3).
* Fluent mapping profiles (since version 1.4). 

**Key features**:
* Calculated properties with custom rules.
* Random failures.



**Links**
* [`Project documentation`](https://github.com/akovanev/DataGenerator/wiki)
* [Integration tests demo](https://github.com/akovanev/DataGenerator/blob/master/Akov.DataGenerator.Demo/StudentsSampleTests/Tests/StudentServiceTests.cs)

**History**

Originally this library was created in order to prepare json mocked data for testing api, which was under development. Api was supposed to aggregate data from a bunch of backend systems, while the client had to import several hundred thousand objects to a new frontend cms. It was extremely important to make sure that all data format surprises, which we were getting, could have been handled properly. That's why **random failures** were added to `DG`. As well as random sized arrays. At the same time, it was impossible to import an object if all properties contained completely random data. This is where **calculated properties** hepled. 

After that I didn't work with `DG` for almost two years. During this time a couple of nice tools, helping out with fake data, appeared. Some time ago, I was working on the other issue, where data couldn't be too random again. I realized, that `DG` may get its second chance and got back to updating it with new features, primarily focusing on building a more convenient setup, based on the fluent syntax.

`DG`, at this moment, is quite far from being optimal in terms of performance as well as in code experience, test coverage or documentation. But if you see that there could be nice features added, feel free to create a github [issue](https://github.com/akovanev/DataGenerator/issues) or [discussion](https://github.com/akovanev/DataGenerator/discussions). 

**Example**
```csharp
// In StudentsTestProfile profile constructor
ForType<Student>()
    .Ignore(s => s.HasWarnings).Ignore(s => s.IsValid)
    .Ignore(s => s.ParsingErrors).Ignore(s => s.ParsingWarnings)
    .Property(s => s.Id).Failure(nullable: 0.2)
    .Property(s => s.FirstName).FromFile("firstnames.txt").Failure(nullable: 0.1)
    .Property(s => s.LastName).FromFile("lastnames.txt").Failure(nullable: 0.1)
    .Property(s => s.FullName).Assign(s => $"{s.FirstName} {s.LastName}")
    .Property(s => s.Year).UseGenerator("MyUintGenerator").Range(5)
    .Property(s => s.Variant).HasJsonName("test_variant")
    .Property(s => s.TestAnswers).HasJsonName("test_answers").Length(5).Range(1, 5)
    .Property(s => s.EncodedSolution).HasJsonName("encoded_solution")
        .Pattern("abcdefghijklmnopqrstuvwxyz0123456789").Length(15, 50).Spaces(1,3)
        .Failure(0.1, 0.1, 0.05, "####-####-####" )
    .Property(s => s.LastUpdated).HasJsonName("last_updated").Pattern("dd/MM/yy")
        .Range("20/10/19","01/01/20").Failure(0.2, 0.2, 0.1)
    .Property(s => s.Subjects).Length(4);

// Execute 
var dg = new DG(new StudentGeneratorFactory(), new DataSchemeMapperConfig { UseCamelCase = true });
string jsonData = dg.GenerateJson<StudentCollection>(new StudentsTestProfile());
```

