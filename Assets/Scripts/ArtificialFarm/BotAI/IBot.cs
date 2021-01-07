using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using JetBrains.Annotations;

namespace ArtificialFarm.BotAI
{
    public interface IBot : IFarmObject
    {
        uint Id { get; }
        ushort Age { get; }
        float Energy { get; set; }

        Genome Genome { get; }


        bool IsAlive { get; }
        bool IsGhost { get; }

        void OnStep();
        bool OnBirth([CanBeNull] params IBot[] parents);
        void OnDeath([CanBeNull] IBot killer);
    }
}