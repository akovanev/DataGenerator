namespace DataGenerator.Demo.Console;

public class StudentGroup
{
    public string Name { get; set; } = null!;
    public string[] ContactPhones { get; set; } = [];
    public Dictionary<string, string> CourseTeachers { get; set; } = new();
    public List<Student>? Students { get; set; }
}

public class Student
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDay { get; set; }
    public required string Email { get; set; }
    public Contact? Contact { get; set; }
    public int? Year { get; set; }
    public Course[] Courses { get; set; } = [];
    public GradeLevel Grade { get; set; }
    public string? Note { get; set; }
}

public class Contact
{
    public required string Phone { get; set; }
    public required string Address { get; set; }
    public string? IpAddress { get; set; }
}

public class Course
{
    public required string Name { get; set; }
    public int Credits { get; set; }
    public DateTimeOffset StartedAt { get; set; }
}

public enum GradeLevel
{
    Freshman,
    Junior,
    Senior
}