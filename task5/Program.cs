using task5.Generators;
using task5.Services;
using Task5.Generators;
using Task5.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<LikesService>();
builder.Services.AddSingleton<SongGenerator>();
builder.Services.AddSingleton<TitleGenerator>();
builder.Services.AddSingleton<EnglishData>();
builder.Services.AddSingleton<GermanData>();
builder.Services.AddSingleton<LanguageFactory>();
builder.Services.AddSingleton<TitleGenerator>();
builder.Services.AddSingleton<AudioGenerator>();
builder.Services.AddSingleton<CoverGenerator>();
builder.Services.AddSingleton<AlbumGenerator>();
builder.Services.AddSingleton<ArtistGenerator>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
