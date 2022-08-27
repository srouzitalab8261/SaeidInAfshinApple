public class Func4
{
    static Func<string, bool> HasThree = str => str.Length == 3;

    static string[] words =
    {
        "sky", "forest", "wood", "cloud", "falcon", "owl", "ocean",
        "water", "bow", "tiny", "arc"
    };

    public IEnumerable<string> threeLetterWords = words.Where(HasThree);

    public void PrintThreeLetterWords(IEnumerable<string> TLW)
    {
        int i = 0;
        Console.WriteLine("Method in class Func4");

        foreach (var words in TLW)
        {
            i += 1;
            Console.WriteLine("{0}: {1}",i,words);
        }
    }
      
}   