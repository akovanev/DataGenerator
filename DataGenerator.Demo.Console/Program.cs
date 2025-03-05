using System.Text.Json;
using Akov.DataGenerator;
using Akov.DataGenerator.FluentSyntax;
using Akov.DataGenerator.Generators;
using Akov.DataGenerator.Utilities;
using DataGenerator.Demo.Console;

var scheme = new DataScheme();

//StudentGroup
scheme.ForType<StudentGroup>()
    .Property(s => s.Name)
        .Template("[resource:Nouns]")
        .Decorate(r => Utility.CapitalizeFirstLetter(r?.ToString()))
    .Property(s => s.Students)
        .Count(3, 5);

//Student
scheme.ForType<Student>()
    .Property(s => s.FirstName)
        .Template("[resource:Firstnames:3-4]")
    .Property(s => s.LastName)
        .Template("[file:lastnames.txt:4-6]")
        .Nullable(0.25)
    .Property(s => s.Email)
        .Construct(s => $"{Utility.BuildNameWithoutSpaces(s.FirstName, s.LastName)}" + 
                        $"{TemplateProcessor.CreateValue(Random.Shared,"@[resource:Domains]")}")
    .Property(s => s.BirthDay)
        .Range(DateTime.Today.AddYears(-60), DateTime.Today.AddYears(-16))
        .Nullable(0.1)
    .Property(s => s.Year)
        .Range(1,5)
        .GenerationRule("NegativeYear", 0.5, _ => Random.Shared.Next(-5, -1))
    .Property(s => s.Courses)
        .Count(2,4)
    .Property(s => s.Note)
        .Template("Note: [number:100-999] [resource:Firstnames] [file:lastnames.txt]");

//Contact
scheme.ForType<Contact>()
    .Property(s => s.Phone)
        .UseGenerator(new PhoneGenerator())
        .PhoneMask("## ## ## ##")
    .Property(s => s.Address)
        .Template("[resource:Addresses]")
    .Property(s => s.IpAddress)
        .UseGenerator(new IpGenerator())
        .Nullable(0.5);

var dataGenerator = new SimpleDataGenerator(scheme);
var randomStudentCollection = dataGenerator.GenerateForType<StudentGroup>();

string json = JsonSerializer.Serialize(
    randomStudentCollection, new JsonSerializerOptions { WriteIndented = true });

Console.WriteLine(json);
file static class Utility
{
    public static string? CapitalizeFirstLetter(string? input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        return char.ToUpper(input[0]) + input[1..];
    }

    public static string BuildNameWithoutSpaces(string firstName, string? lastName)
        => (firstName + lastName?.Replace(" ", "")).ToLower();
}
