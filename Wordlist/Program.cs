namespace Wordlist;

public static class Program
{
    static void Main()
    {
        string filePath = Path.Combine("Data", "wordlist.txt");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("Error: wordlist.txt not found.");
            return;
        }

        var processor = new WordlistProcessor(filePath);
        _ = processor.FindConcatenatedWords(6).ToList();
    }
}

public class WordlistProcessor(string filePath)
{
    private readonly HashSet<string> words = new(File.ReadLines(filePath).Select(line => line.Trim().ToLower()));

    public IEnumerable<string> FindConcatenatedWords(int length) => words.Where(word => word.Length == length && HasTwoValidParts(word));

    private bool HasTwoValidParts(string word)
    {
        for (int i = 1; i < word.Length; i++)
        {
            string part1 = word[..i];
            string part2 = word[i..];
            if (words.Contains(part1) && words.Contains(part2))
            {
                Console.WriteLine($"{part1} + {part2} => {word}");
                return true;
            }
        }
        return false;
    }
}