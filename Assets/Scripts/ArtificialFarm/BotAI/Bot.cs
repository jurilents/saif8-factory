<<<<<<< HEAD
using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using UnityEngine;
=======
using ArtificialFarm.FarmMap;
>>>>>>> parent of 7347170... v0.1.0

namespace ArtificialFarm.BotAI
{
    /// <summary>
<<<<<<< HEAD
    /// Main class for each alive organism on the farm
    /// </summary>
    public class Bot : FarmObject, IBot
    {
        public Genome Genome { get; private set; }


=======
    /// 
    /// </summary>
    public class Bot : FarmObject, IBot
    {
>>>>>>> parent of 7347170... v0.1.0
        /// <summary>
        /// 
        /// </summary>
        public ushort Age { get; private set; }
<<<<<<< HEAD

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
=======
        
        /// <summary>
        /// 
        /// </summary>
        public float Energy { get; private set; }

        public bool IsAlive { get; }
>>>>>>> parent of 7347170... v0.1.0
        public bool IsGhost { get; }


        /// <summary>
        /// Performed on each iteration of farm life loop
        /// </summary>
        public void OnStep()
        {
<<<<<<< HEAD
            var next = Genome.Next();
            next.Apply(this);
            Age++;
        }

=======
        }
        
>>>>>>> parent of 7347170... v0.1.0

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parents"></param>
        public void OnBirth(IBot[] parents)
        {
<<<<<<< HEAD
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


=======
            if (parents.Length != null)
            {
                
            }

            Energy = 50;
        }

>>>>>>> parent of 7347170... v0.1.0
        /// <summary>
        /// Performed on bot death
        /// </summary>
        /// <param name="killer"></param>
        public void OnDeath(IBot killer)
        {
<<<<<<< HEAD
            // Cell.Bot = null;
            Cell.Content = CellContentType.DeadBody;
            Cell = null;

            string killerName = killer is null ? "age factor" : killer.ToString();
            Debug.Log($"{this} was killed via {killerName}");
        }


        public override string ToString() => $"BOT#{Id}";
=======
        }

        public override string ToString() =>
            $"{GetType().Name}#{Id}";
>>>>>>> parent of 7347170... v0.1.0
    }
}