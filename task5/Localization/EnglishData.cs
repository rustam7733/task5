using task5.Localization;

namespace Task5.Localization;

public class EnglishData : ILanguageData
{
    public string[] Adjectives =>
    [
        "Midnight","Broken","Golden","Lonely","Silent","Wild",
        "Electric","Endless","Falling","Neon","Burning","Lost"
    ];

    public string[] Nouns =>
    [
        "Dreams","Lights","Echo","Hearts","Fire","Sky",
        "Road","City","Shadows","Voices","Stars","Memories"
    ];

    public string[] Places =>
    [
        "Berlin","London","Tokyo","New York","Paris"
    ];

    public string[] Verbs =>
    [
        "Running","Fading","Burning","Dancing","Falling","Rising"
    ];

    public string[] Genres =>
    [
        "Rock","Pop","Electronic","Jazz","Hip-Hop","Indie"
    ];
}