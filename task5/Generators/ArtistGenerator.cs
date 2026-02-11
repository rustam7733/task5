using Bogus;

namespace Task5.Generators;

public class ArtistGenerator
{
    public string Generate(Random random, string locale)
    {
        var faker = new Faker(locale);

        if (random.NextDouble() < 0.5)
        {
            return faker.Name.FullName();
        }

        return BuildBandName(random, faker);
    }

    private static string BuildBandName(Random random, Faker faker)
    {
        string[] prefixes =
        {
        "The","Neon","Silent","Electric","Broken","Midnight","Golden","Dark"
    };

        string[] nouns =
        {
        "Echo","Voices","Dreams","Lights","Shadows","Hearts","Waves","Stars","Fire","Sky"
    };

        int pattern = random.Next(3);

        if (pattern == 0)
        {
            string p = prefixes[random.Next(prefixes.Length)];
            string n = nouns[random.Next(nouns.Length)];

            while (p == n)
                n = nouns[random.Next(nouns.Length)];

            return $"{p} {n}";
        }

        if (pattern == 1)
            return nouns[random.Next(nouns.Length)];

        return $"{prefixes[random.Next(prefixes.Length)]} {faker.Random.Word()}";
    }
}