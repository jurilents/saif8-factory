namespace ArtificialFarm.BotAI.Genetic
{
    public enum Diet : sbyte
    {
        None = -1,
        PhotoPlant = 0, // eating only sun energy
        MineralsPlant,  // eating minerals from bottom
        HerbivAnimal,   // eating "PhotoPlant" and "MineralsPlant"
        PredatorAnimal, // eating only "HerbivAnimal"
        // OmnivorAnimal,  // eating every kind of bots
    }
}