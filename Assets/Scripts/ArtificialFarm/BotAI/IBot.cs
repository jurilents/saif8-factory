<<<<<<< HEAD
using ArtificialFarm.BotAI.Genetic;
=======
using ArtificialFarm.FarmMap;
>>>>>>> parent of 7347170... v0.1.0
using JetBrains.Annotations;

namespace ArtificialFarm.BotAI
{
    public interface IBot
    {
        uint Id { get; }
        ushort Age { get; }
        float Energy { get; }

<<<<<<< HEAD
        Genome Genome { get; }

        bool IsAlive { get; }
        bool IsGhost { get; }

=======
        Population Pop { get; }
        WorldMap Map { get; }
        
        Cell Cell { get; set; }
        Turn Turn { get; }

        bool IsAlive { get; }
        bool IsGhost { get; }
        
>>>>>>> parent of 7347170... v0.1.0
        void OnStep();
        void OnBirth(params IBot[] parents);
        void OnDeath([CanBeNull] IBot killer);
    }
}