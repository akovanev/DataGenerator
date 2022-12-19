namespace Akov.DataGenerator.Extensions;

public static class StringExtensions
{
    public static (int, string) GetSplitSizeOrString(this string source, string separator, int substring = -1)
    {
        int count = 0;
        int prev = 0;
        int i;

        for (i = 0; i < source.Length; i++)
        {
            if (source[i] != separator[0]) continue;

            bool isMatch = true;

            for (int j = 1; j < separator.Length; j++)
            {
                if (source[i + j] == separator[j]) continue;
                
                isMatch = false;
                break;
            }

            if (!isMatch) continue;

            if (count == substring)
                return (count + 1, source.Substring(prev, i - prev));

            if (i + separator.Length == source.Length) break;
            
            prev = i + separator.Length;
            i = prev - 1;
            count++;
        }

        return (count + 1, source[prev..]);
    }
}