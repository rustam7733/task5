namespace Task5.Generators;

public class AlbumGenerator
{
    public string Generate(Random random)
    {
        if (random.NextDouble() < 0.5)
            return "Single";

        int pattern = random.Next(5);

        if (pattern == 0)
            return BuildAdjNoun(random);

        if (pattern == 1)
            return BuildOfPattern(random);

        if (pattern == 2)
            return $"{Pick(Nouns, random)} Vol. {random.Next(1, 5)}";

        if (pattern == 3)
            return $"Live in {Pick(Cities, random)}";

        return $"{Pick(Adjectives, random)} Collection";
    }

    private string BuildAdjNoun(Random random)
    {
        string adj = Pick(Adjectives, random);
        string noun = Pick(Nouns, random);

        while (adj == noun)
            noun = Pick(Nouns, random);

        return $"{adj} {noun}";
    }

    private string BuildOfPattern(Random random)
    {
        string first = Pick(Nouns, random);
        string second = Pick(Nouns, random);

        while (second == first)
            second = Pick(Nouns, random);

        return $"{first} of {second}";
    }

    private string Pick(string[] arr, Random random)
        => arr[random.Next(arr.Length)];

    private static readonly string[] Adjectives =
    {
        "Midnight","Silent","Golden","Broken","Neon","Dark","Electric","Lonely","Endless"
    };

    private static readonly string[] Nouns =
    {
        "Dreams","Echo","Lights","Voices","Shadows","Hearts","Fire","Sky","Memories","Waves"
    };

    private static readonly string[] Cities =
    {
        "Berlin","London","Paris","Tokyo","New York","Amsterdam"
    };
}