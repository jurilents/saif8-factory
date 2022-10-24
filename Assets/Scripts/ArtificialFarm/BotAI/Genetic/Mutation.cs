namespace ArtificialFarm.BotAI.Genetic
{
    public class Mutation
    {
        public bool Enabled { get; set; }

        public float Chance { get; }
        public int Count { get; }


        public Mutation(float chance, int count)
        {
            Chance = chance;
            Count = count;
        }
    }
}