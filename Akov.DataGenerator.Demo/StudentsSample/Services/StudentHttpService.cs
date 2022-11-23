using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSample.ApiClients;
using Akov.DataGenerator.Demo.StudentsSample.Responses;

namespace Akov.DataGenerator.Demo.StudentsSample.Services;

/// <summary>
/// The service simulating retrieving the student list.
/// </summary>
public class StudentHttpService
{
    private readonly ApiClient _apiClient;

    public StudentHttpService(HttpClient httpClient)
    {
        _apiClient = new ApiClient(httpClient);
    }

    public async Task<IEnumerable<Student>?> GetAll()
    {
        var result = await _apiClient.GetAsync<StudentCollection>(
            "https://example.com/api/students");

        if (result?.Students is null) return null;

        Log(result.Students);

        //Only valid student objects are to return.
        return result.Students
            .Where(s => s.IsValid)
            .ToList();
    }

    //Logs errors and warnings from the student list.
    //The idea is to show on which level invalid objects may be handled.
    internal void Log(IEnumerable<Student> students)
    {
        foreach (var student in students)
        {
            if(!student.IsValid)
                LogDictionary(student.ParsingErrors);
            if (student.HasWarnings)
                LogDictionary(student.ParsingWarnings);
        }
    }

    internal void LogDictionary(Dictionary<string, string> dictionary)
    {
        foreach (var item in dictionary)
        {
            Debug.WriteLine(item.Value);
        }
    }
}