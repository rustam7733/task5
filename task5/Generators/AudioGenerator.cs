using System.Text;

namespace Task5.Generators;

public class AudioGenerator
{
    public byte[] Generate(long seed, int page, int index)
    {
        var songSeed = HashCode.Combine(seed, page, index);
        var random = new Random(songSeed);

        int sampleRate = 44100;
        int durationSeconds = 3;
        int samplesCount = sampleRate * durationSeconds;

        double baseFreq = 180 + random.Next(0, 200);

        double[] melody =
        {
            baseFreq,
            baseFreq * 1.25,
            baseFreq * 1.5,
            baseFreq * 2
        };

        short[] samples = new short[samplesCount];

        for (int i = 0; i < samplesCount; i++)
        {
            double t = (double)i / sampleRate;
            double freq = melody[(i / 8000) % melody.Length];

            double wave =
                Math.Sin(2 * Math.PI * freq * t) * 0.6 +
                Math.Sin(2 * Math.PI * (freq / 2) * t) * 0.3;

            samples[i] = (short)(wave * short.MaxValue);
        }

        return BuildWav(samples, sampleRate);
    }

    private byte[] BuildWav(short[] samples, int sampleRate)
    {
        using var ms = new MemoryStream();
        using var writer = new BinaryWriter(ms, Encoding.UTF8);

        int byteRate = sampleRate * 2;

        writer.Write(Encoding.ASCII.GetBytes("RIFF"));
        writer.Write(36 + samples.Length * 2);
        writer.Write(Encoding.ASCII.GetBytes("WAVE"));

        writer.Write(Encoding.ASCII.GetBytes("fmt "));
        writer.Write(16);
        writer.Write((short)1);
        writer.Write((short)1);
        writer.Write(sampleRate);
        writer.Write(byteRate);
        writer.Write((short)2);
        writer.Write((short)16);

        writer.Write(Encoding.ASCII.GetBytes("data"));
        writer.Write(samples.Length * 2);

        foreach (var sample in samples)
            writer.Write(sample);

        writer.Flush();
        return ms.ToArray();
    }
}