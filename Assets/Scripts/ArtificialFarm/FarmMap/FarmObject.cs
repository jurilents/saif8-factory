using ArtificialFarm.BotAI;

namespace ArtificialFarm.FarmMap
{
    public class FarmObject
    {
        public uint Id { get; }

        public Population Pop { get; }
        public WorldMap Map { get; }
        public Cell Cell { get; set; }
        public Turn Turn { get; }
    }
}