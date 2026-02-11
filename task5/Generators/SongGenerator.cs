using Bogus;
using task5.Models;
using task5.Services;
using Task5.Generators;
using Task5.Localization;

namespace task5.Generators
{
    public class SongGenerator(
        LikesService likesService,
        TitleGenerator titleGenerator,
        LanguageFactory languageFactory,
        ArtistGenerator artistGenerator,
        AlbumGenerator albumGenerator)
    {
        public List<Song> Generate(long seed, int page, string locale, double avgLikes, int pageSize = 20)
        {
            var lang = languageFactory.Get(locale);

            var baseSeed = seed ^ page;

            var contentRandom = new Random((int)(baseSeed & 0x7fffffff));
            var likesRandom = new Random((int)((baseSeed ^ 999999) & 0x7fffffff));

            Randomizer.Seed = contentRandom;

            var songs = new List<Song>();

            for (int i = 0; i < pageSize; i++)
            {
                var song = new Song
                {
                    Index = (page - 1) * pageSize + i + 1,
                    Title = titleGenerator.Generate(contentRandom, lang),
                    Genre = lang.Genres[contentRandom.Next(lang.Genres.Length)],
                    Artist = artistGenerator.Generate(contentRandom, locale),
                    Album = albumGenerator.Generate(contentRandom),
                    Likes = likesService.Generate(avgLikes, likesRandom)
                };

                songs.Add(song);
            }

            return songs;
        }
    }
}
