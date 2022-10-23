using System;
using System.Linq;
using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using UnityEngine;
using Utilities;

namespace ArtificialFarm.BotAI
{
    /// <summary>
    /// Main class for each alive organism on the farm
    /// </summary>
    public class Bot : FarmObject, IBot
    {
        public Genome Genome { get; private set; }

        public ushort Age { get; private set; }

        private float _energy;

        public float Energy
        {
            get => _energy;
            set => _energy = Mathf.Clamp(value, -1, 100);
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAlive { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGhost { get; private set; }


        /// <summary>
        /// Performed on each iteration of farm life loop
        /// </summary>
        public void OnStep()
        {
            IsAlive = Energy > 0 && Age < 50;
            if (IsAlive)
            {
                Age++;
                var gene = Genome.Next();
                gene.Apply(this);
                // Debug.Log($"GENE: {gene.Name}\nage:{Age} Pow:{Energy}");
            }
            else Age--;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">Kind of display mode (^~^)</param>
        /// <returns>Tile color to display</returns>
        public override Color OnDisplay(DisplayMode mode)
        {
            switch (mode)
            {
                case DisplayMode.Default:
                    return Color.yellow;

                case DisplayMode.Energy:
                    if (!IsAlive) return Color.yellow;
                    byte val = (byte) (Energy / 120 * 255);
                    // Debug.Log(val);
                    return new Color32(val, 0, val, 255);

                case DisplayMode.Diet:
                    return Genome.Diet switch
                    {
                        Diet.PhotoPlant => new Color32(140, 181, 65, 255),
                        Diet.MineralsPlant => new Color32(64, 165, 192, 255),
                        Diet.HerbivAnimal => new Color32(227, 126, 50, 255),
                        Diet.PredatorAnimal => new Color32(186, 82, 227, 255),
                        // Diet.OmnivorAnimal => Color.magenta,
                        _ => throw new ArgumentOutOfRangeException("Unexpected diet type: " + Genome.Diet)
                    };

                case DisplayMode.Health:
                default:
                    return Color.red;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="parents"></param>
        /// <returns>Does operation was succeeded</returns>
        public bool OnBirth(IBot[] parents)
        {
            if (parents is null) // On first spawn
            {
                Cell = Map.GetRandomEmptyCell();
                Genome = new Genome();
            }
            else // On birth from parent(s)
            {
                var mother = parents[0];

                // var emptyNeighboured = Map.GetNeighbors(mother.Cell.Pos)
                //     .Select(n => Map.GetCell(n))
                //     .Where(c => c.ContentType is CellContentType.Void).ToList();
                // if (emptyNeighboured.Count == 0) return false;
                // Cell = RandMe.RandItem(emptyNeighboured);

                Cell = Map.GetCellByMove(mother.Cell, mother.Turn);
                if (Cell.ContentType != CellContentType.Void)
                {
                    Cell = null;
                    return false;
                }

                Genome = new Genome(mother.Genome);
            }

            Cell.SetContent(CellContentType.Organism, this);
            Age = 0;
            Energy = 120;
            IsAlive = true;
            IsGhost = false;
            return true;
        }

        /// <summary>
        /// Performed on bot death
        /// </summary>
        /// <param name="killer"></param>
        public void OnDeath(IBot killer)
        {
            Cell.SetContent(CellContentType.Void);

            string killerName = killer is null ? "age factor or exhaustion" : killer.ToString();
            // Debug.Log($"{this} was killed via {killerName}");
        }


        public override string ToString() => $"BOT#{Id}";
    }
}