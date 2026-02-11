using task5.Localization;
using Task5.Localization;

namespace Task5.Generators;

public class TitleGenerator
{
    public string Generate(Random random, ILanguageData lang)
    {
        int pattern = random.Next(4);

        return pattern switch
        {
            0 => $"{PickDifferent(lang.Adjectives, lang.Nouns, random)} {Pick(lang.Nouns, random)}",
            1 => $"{Pick(lang.Verbs, random)} {Pick(lang.Nouns, random)}",
            2 => BuildOfPattern(random, lang),
            3 => $"{Pick(lang.Nouns, random)} in {Pick(lang.Places, random)}",
            _ => "Untitled"
        };
    }

    private static string BuildOfPattern(Random random, ILanguageData lang)
    {
        string first = Pick(lang.Nouns, random);
        string second = Pick(lang.Nouns, random);

        while (second == first)
            second = Pick(lang.Nouns, random);

        return $"{first} of {second}";
    }

    private static string Pick(string[] array, Random random)
        => array[random.Next(array.Length)];

    private static string PickDifferent(string[] a, string[] b, Random random)
    {
        string value = a[random.Next(a.Length)];
        string noun = b[random.Next(b.Length)];

        while (value == noun)
            noun = b[random.Next(b.Length)];

        return value;
    }
}