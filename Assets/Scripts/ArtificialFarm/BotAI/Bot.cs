using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using UnityEngine;

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
        public bool IsAlive { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGhost { get; }


        /// <summary>
        /// Performed on each iteration of farm life loop
        /// </summary>
        public void OnStep()
        {
            Genome.Next().Apply(this);

            IsAlive = Energy > 0 && Age < 25;
            if (IsAlive) Age++;
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
        public void OnBirth(IBot[] parents)
        {
            if (parents.Length < 1)
            {
                Cell = Map.GetRandomEmptyCell();
                Genome = new Genome();
                Genome.InitRandomly<Bot>();
            }
            else
            {
                Debug.Log("New burn!");
            }

            Cell.SetContent(CellContentType.Organism, this);
            Energy = 50;
        }

        /// <summary>
        /// Performed on bot death
        /// </summary>
        /// <param name="killer"></param>
        public void OnDeath(IBot killer)
        {
            // Cell.Bot = null;
            Cell.SetContent(CellContentType.DeadBody);
            Cell = null;

            string killerName = killer is null ? "age factor" : killer.ToString();
            Debug.Log($"{this} was killed via {killerName}");
        }


        public override string ToString() => $"BOT#{Id}";
    }
}