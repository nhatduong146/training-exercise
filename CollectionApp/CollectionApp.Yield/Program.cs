public class Program
{
    public static async IAsyncEnumerable<string> FindLinesWithKeywordAsync(string filePath, string keyword)
    {
        using var reader = new StreamReader(filePath);

        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            Console.WriteLine("Read new line");
            if (line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                yield return line; 
        }
    }

    public static async Task Main(string[] args)
    {
        var filePath = "D:\\CDP\\Source\\TrainingExcercise\\CollectionApp\\CollectionApp.Yield\\ServerLog.txt";
        var keyword = "user";

        var expectedLines = 1;
        var resultLines = 0;

        await foreach (var line in FindLinesWithKeywordAsync(filePath, keyword))
        {
            Console.WriteLine(line);
            if (++resultLines >= expectedLines)
                break;
        }
    }
}
