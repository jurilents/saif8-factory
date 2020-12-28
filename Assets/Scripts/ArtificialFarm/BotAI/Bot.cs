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
        public float Energy { get; protected internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAlive { get; private set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool IsGhost { get; }


        /// <summary>
        /// Performed on each iteration of farm life loop
        /// </summary>
        public void OnStep()
        {
            IsAlive = Energy > 0 && Age < 25;
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
                    return new Color((byte) (Energy * 2.55), 0, 128, 255);

                case DisplayMode.Diet:
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
            if (parents is null)
            {
                Cell = Map.GetRandomEmptyCell();
                Genome = new Genome();
            }
            else
            {
                var mother = parents[0];

                var emptyNeighboured = Map.GetNeighbors(mother.Cell.Pos)
                    .Select(n => Map.GetCell(n))
                    .Where(c => c.ContentType is CellContentType.Void).ToList();
                if (emptyNeighboured.Count == 0) return false;

                Cell = RandMe.RandItem(emptyNeighboured);
                Genome = new Genome(mother.Genome);
            }

            Cell.SetContent(CellContentType.Organism, this);
            Energy = 500;
            return true;
        }

        /// <summary>
        /// Performed on bot death
        /// </summary>
        /// <param name="killer"></param>
        public void OnDeath(IBot killer)
        {
            // Cell.Bot = null;
            Cell.SetContent(CellContentType.DeadBody);

            string killerName = killer is null ? "age factor or exhaustion" : killer.ToString();
            // Debug.Log($"{this} was killed via {killerName}");
        }


        public override string ToString() => $"BOT#{Id}";
    }
}