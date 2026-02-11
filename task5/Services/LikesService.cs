namespace task5.Services
{
    public class LikesService
    {
        public int Generate(double avgLikes, Random random)
        {
            int baseLikes = (int)Math.Floor(avgLikes);
            double fraction = avgLikes - baseLikes;

            if (random.NextDouble() < fraction)
                baseLikes++;

            return baseLikes;
        }
    }
}
