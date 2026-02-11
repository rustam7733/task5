using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.Fonts;
using Microsoft.AspNetCore.Hosting;

namespace Task5.Generators;

public class CoverGenerator
{
    private readonly Font _titleFont;
    private readonly Font _artistFont;

    public CoverGenerator(IWebHostEnvironment env)
    {
        var collection = new FontCollection();
        var fontPath = Path.Combine(env.ContentRootPath, "Assets", "Fonts", "AAdemyacttItalic.ttf");
        var family = collection.Add(fontPath);

        _titleFont = family.CreateFont(54, FontStyle.Bold);
        _artistFont = family.CreateFont(33);
    }

    public byte[] Generate(long seed, int index, string album, string artist)
    {
        var albumSeed = HashCode.Combine(seed, album);
        var random = new Random(albumSeed);

        using var image = new Image<Rgba32>(512, 512);

        int style = random.Next(4);

        var c1 = new Rgba32((byte)random.Next(20, 230), (byte)random.Next(20, 230), (byte)random.Next(20, 230));
        var c2 = new Rgba32((byte)random.Next(20, 230), (byte)random.Next(20, 230), (byte)random.Next(20, 230));

        if (style == 0)
            image.Mutate(x => x.Fill(c1));
        else if (style == 1)
            image.Mutate(x => x.Fill(new LinearGradientBrush(
                new PointF(0, 0),
                new PointF(512, 512),
                GradientRepetitionMode.None,
                new ColorStop(0, c1),
                new ColorStop(1, c2)
            )));
        else if (style == 2)
        {
            image.Mutate(x => x.Fill(c1));
            for (int i = 0; i < 40; i++)
            {
                var rect = new Rectangle(
                    random.Next(0, 512),
                    random.Next(0, 512),
                    random.Next(40, 160),
                    random.Next(40, 160)
                );
                image.Mutate(x => x.Fill(c2, rect));
            }
        }
        else
        {
            image.Mutate(x => x.Fill(c1));
            for (int i = 0; i < 5000; i++)
            {
                var px = random.Next(0, 512);
                var py = random.Next(0, 512);
                image[px, py] = c2;
            }
        }

        image.Mutate(x =>
        {
            x.DrawText(album, _titleFont, Color.White, new PointF(40, 220));
            x.DrawText(artist, _artistFont, Color.LightGray, new PointF(40, 280));
        });

        using var ms = new MemoryStream();
        image.SaveAsPng(ms);
        return ms.ToArray();
    }
}