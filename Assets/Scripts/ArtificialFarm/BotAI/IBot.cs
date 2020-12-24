using ArtificialFarm.BotAI.Genetic;
using JetBrains.Annotations;

namespace ArtificialFarm.BotAI
{
    public interface IBot
    {
        uint Id { get; }
        ushort Age { get; }
        float Energy { get; }

        Genome Genome { get; }

        bool IsAlive { get; }
        bool IsGhost { get; }

        void OnStep();
        void OnBirth(params IBot[] parents);
        void OnDeath([CanBeNull] IBot killer);
    }
}