using task5.Localization;

namespace Task5.Localization;

public class GermanData : ILanguageData
{
    public string[] Adjectives =>
    [
        "Mitternacht","Verloren","Goldene","Einsam","Still","Wild",
        "Elektrisch","Endlos","Fallend","Neon","Brennend","Dunkel"
    ];

    public string[] Nouns =>
    [
        "Träume","Lichter","Echo","Herzen","Feuer","Himmel",
        "Straße","Stadt","Schatten","Stimmen","Sterne","Erinnerungen"
    ];

    public string[] Places =>
    [
        "Berlin","Hamburg","München","Köln","Frankfurt"
    ];

    public string[] Verbs =>
    [
        "Laufend","Fallend","Tanzend","Steigend","Brennend"
    ];

    public string[] Genres =>
    [
        "Rock","Pop","Elektronisch","Jazz","Hip-Hop","Indie"
    ];
}
