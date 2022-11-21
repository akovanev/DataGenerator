using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSample.Responses;

namespace Akov.DataGenerator.Demo.StudentsSample.Services;

/// <summary>
/// The service simulating retrieving the student list.
/// </summary>
public class StudentRepoService
{
    private readonly IStudentRepository _studentRepository;

    public StudentRepoService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<IEnumerable<Student>?> GetAll()
    {
        var students = await _studentRepository.GetAll();

        //Only valid student objects are to return.
        return students?
            .Where(s => s.IsValid)
            .ToList();
    }
}

public interface IStudentRepository
{
    Task<IEnumerable<Student>?> GetAll();
}