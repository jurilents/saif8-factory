using ArtificialFarm.BotAI.Genetic;

namespace ArtificialFarm.BotIntelligences
{
    public partial class TypeAlpha 
    {
        public TypeAlpha() : base(new Genome<TypeAlpha>())
        { }

        private TypeAlpha(TypeAlpha parent) : base(new Genome<TypeAlpha>(parent._genome))
        { }
    }
}