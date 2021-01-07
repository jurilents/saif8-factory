using ArtificialFarm.Core;
using UnityEngine;

namespace ArtificialFarm.BotAI.Genetic
{
    public class Genome
    {
        private readonly float _actuallyDiet;

        public Diet Diet { get; }


        private GeneticCore _dnaBase;
        private byte[] _genes;
        private int _index;

        public Genome()
        {
            _dnaBase = FarmSettings.Current.Dna[FarmSettings.DefaultGenes];
            _genes = _dnaBase.GenerateGenome();
            Diet = (Diet) _actuallyDiet;
        }


        public Genome(Genome parent)
        {
            _dnaBase = parent._dnaBase;
            _genes = (byte[]) parent._genes.Clone();
            _dnaBase.Mutate(_genes);

            // try to mutate diet (smoothly)
            _actuallyDiet = _dnaBase.MutateDietValue(parent._actuallyDiet, 0.1f, 0.125f);
            Diet = (Diet) Mathf.Clamp(_actuallyDiet, 0, 3);
        }


        public Gene Next()
        {
            // loop index value in available range
            _index = _index >= _genes.Length - 1 ? 0 : _index + 1;
            // get gene by byte identifier value
            return _dnaBase[_genes[_index]];
        }
    }
}