using task5.Localization;

namespace Task5.Localization;

public class LanguageFactory(EnglishData english, GermanData german)
{
    public ILanguageData Get(string locale)
    {
        return locale switch
        {
            "de" or "de-DE" => german,
            _ => english
        };
    }
}
