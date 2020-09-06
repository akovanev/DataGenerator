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

            if (result?.Students is null) return null;

            Log(result.Students);

            return result.Students
                .Where(s => s.IsValid)
                .ToList();
        }

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
            Debug.WriteLine(
                string.Join(
                    ",", 
                    dictionary.SelectMany(x => x.Value)));
        }
    }
}
