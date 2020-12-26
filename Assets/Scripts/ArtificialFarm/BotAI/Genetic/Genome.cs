using ArtificialFarm.Core;
using UnityEngine;

namespace ArtificialFarm.BotAI.Genetic
{
    public class Genome
    {
        private GeneticCore _dnaBase;
        private byte[] _genes;
        private int _index;

        public Genome()
        {
            _dnaBase = FarmSettings.Current.Dna[FarmSettings.DefaultGenes];
            _genes = _dnaBase.GenerateGenome();
        }


        public Genome(Genome parent)
        {
            _dnaBase = parent._dnaBase;
            _genes = (byte[]) parent._genes.Clone();
            _dnaBase.Mutate(_genes);
        }

        public Gene Next()
        {
            _index = _index >= _genes.Length - 1 ? 0 : _index + 1;
            return _dnaBase[_genes[_index]];
        }
    }
}