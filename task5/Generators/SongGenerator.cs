using Bogus;
using task5.Models;
using task5.Services;
using Task5.Generators;
using Task5.Localization;

namespace task5.Generators;

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

        var pageRandom = new Random((int)((seed ^ page) & 0x7fffffff));
        var localeSeed = locale.GetHashCode();

        var songs = new List<Song>();

        for (int i = 0; i < pageSize; i++)
        {
            var songSeed = HashCode.Combine(pageRandom.Next(), localeSeed);

            var titleRandom = new Random(songSeed);
            var artistRandom = new Random(songSeed ^ 1);
            var albumRandom = new Random(songSeed ^ 2);
            var genreRandom = new Random(songSeed ^ 3);
            var likesRandom = new Random(songSeed ^ 4);

            var song = new Song
            {
                Index = (page - 1) * pageSize + i + 1,
                Title = titleGenerator.Generate(titleRandom, lang),
                Artist = artistGenerator.Generate(artistRandom, locale),
                Album = albumGenerator.Generate(albumRandom),
                Genre = lang.Genres[genreRandom.Next(lang.Genres.Length)],
                Likes = likesService.Generate(avgLikes, likesRandom)
            };

            songs.Add(song);
        }

        return songs;
    }
}