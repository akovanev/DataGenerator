using Newtonsoft.Json;

namespace Akov.DataGenerator.Extensions
{
    public static class UtilsExtensions
    {
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        public static string ToCamelCase(this string source)
        {
            char first = char.ToLower(source[0]);
            return $"{first}{source.Substring(1)}";
        }
    }
}
