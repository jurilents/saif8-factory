<<<<<<< HEAD
using ArtificialFarm.Core;

namespace ArtificialFarm.BotAI.Genetic
{
    public class Genome
    {
        private GeneticCore _dnaBase;
        private byte[] _genes;
        private int _index;


        public void InitRandomly()
        {
            _dnaBase = FarmSettings.Current.Dna[""];
            _genes = _dnaBase.GenerateGenome();
        }


        public void InitFromParent(Genome parent)
=======
namespace ArtificialFarm.BotAI.Genetic
{
    public class Genome<TBot> where TBot : IBot
    {
        private readonly byte[] _genes;
        private readonly DNABase _dnaBase;
        private int _index;

        public Genome()
        {
            _dnaBase = FarmSettings.Current.Dna[typeof(TBot)];
            _genes = _dnaBase.GenerateGenome();
        }

        public Genome(Genome<TBot> parent)
>>>>>>> parent of 7347170... v0.1.0
        {
            _dnaBase = parent._dnaBase;
            _genes = (byte[]) parent._genes.Clone();
            _dnaBase.Mutate(_genes);
        }

<<<<<<< HEAD

=======
>>>>>>> parent of 7347170... v0.1.0
        public Gene Next()
        {
            _index = _index >= _genes.Length - 1 ? 0 : _index + 1;
            return _dnaBase[_genes[_index]];
        }
    }
}