namespace ArtificialFarm.BotAI.Genetic
{
    public class Mutation
    {
        public bool Enabled { get; set; }

        public float Chance { get; set; }
        public int Count { get; set; }


        public Mutation(float chance, int count)
        {
            Chance = chance;
            Count = count;
        }
    }
}