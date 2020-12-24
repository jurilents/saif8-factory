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


        /// <summary>
        /// 
        /// </summary>
        public ushort Age { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public float Energy { get; protected internal set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAlive { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsGhost { get; }


        /// <summary>
        /// Performed on each iteration of farm life loop
        /// </summary>
        public void OnStep()
        {
            var next = Genome.Next();
            next.Apply(this);
            Age++;
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
                Genome.InitRandomly();
            }
            else
            {
                Debug.Log("New burn!");
            }

            Cell.Content = CellContentType.Organism;
            Energy = 50;
        }


        /// <summary>
        /// Performed on bot death
        /// </summary>
        /// <param name="killer"></param>
        public void OnDeath(IBot killer)
        {
            // Cell.Bot = null;
            Cell.Content = CellContentType.DeadBody;
            Cell = null;

            string killerName = killer is null ? "age factor" : killer.ToString();
            Debug.Log($"{this} was killed via {killerName}");
        }


        public override string ToString() => $"BOT#{Id}";
    }
}