using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm.BotAI.Genetic
{
    public abstract class GeneticBot
    {
        public uint Id { get; }

        public ushort Age { get; private set; }

        private float _energy;

        public float Energy
        {
            get => _energy;
            protected set
            {
                _energy = (byte) Mathf.Clamp(value, -1, 50);
                // Debug.Log($"Bot: age:{_age} pow:{_energyReserve} alive:{IsAlive}");
            }
        }

        public bool IsAlive { get; private set; }
        public bool IsGhost => Age < -10;


        public Population Pop { get; }
        public WorldMap Map { get; }


        private Cell _cell;

        public Cell Cell
        {
            get => _cell;
            set
            {
                _cell.Bot = null;
                _cell = value;
                if (_cell != null) _cell.Bot = this;
            }
        }

        public Turn Turn { get; }

        protected Genome<TBot> _genome { get; }


        protected GeneticBot(Genome<TBot> genome)
        {
            Id = FarmSettings.SummaryPopulation++;
            Pop = FarmSettings.Current.Pop;
            Map = FarmSettings.Current.Map;

            _cell = Map.GetRandomEmptyCell();
            Turn = new Turn(-1);

            _genome = genome;

            Age = 0;
            Energy = 50;
        }


        public void OnStep()
        {
            IsAlive = Energy > 0 && Age < 25;
            if (IsAlive) Age++;
            else Age--;
            
            ApplyGene();
        }


        public void OnDeath(IBot killer)
        {
        }


        protected void ApplyGene()
        {
            var next = _genome.Next();
            // Debug.Log("Next gene: " + next);
            next.Apply(this);
        }


        public override string ToString() =>
            $"{GetType().Name}#{Id}";
    }
}