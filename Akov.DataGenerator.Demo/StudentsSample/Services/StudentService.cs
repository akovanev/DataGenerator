using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSample.ApiClients;
using Akov.DataGenerator.Demo.StudentsSample.Responses;

namespace Akov.DataGenerator.Demo.StudentsSample.Services
{
    public class StudentService
    {
        private readonly ApiClient _apiClient;

        public StudentService(HttpClient httpClient)
        {
            _apiClient = new ApiClient(httpClient);
        }

        public async Task<IEnumerable<Student>?> GetAll()
        {
            var result = await _apiClient.GetAsync<StudentCollection>(
                "https://example.com/api/students");

            if (result is null) return null;

            var students = result.Students
                .Where(s => s.IsValid)
                .ToList();

            var studentsWithErrors = result.Students
                .Except(students)
                .ToList();

            if(studentsWithErrors.Any())
                LogErrors(studentsWithErrors);

            return students;
        }

        internal void LogErrors(IEnumerable<Student> students)
        {
            foreach (var student in students)
            {
                Debug.WriteLine(student.ParsingErrors.ToString());
            }
        }
    }
}
