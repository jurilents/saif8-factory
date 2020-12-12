using ArtificialFarm.FarmMap;

namespace ArtificialFarm.BotAI
{
    /// <summary>
    /// 
    /// </summary>
    public class Bot : FarmObject, IBot
    {
        /// <summary>
        /// 
        /// </summary>
        public ushort Age { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float Energy { get; private set; }

        public bool IsAlive { get; }
        public bool IsGhost { get; }


        /// <summary>
        /// Performed on each iteration of farm life loop
        /// </summary>
        public void OnStep()
        {
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parents"></param>
        public void OnBirth(IBot[] parents)
        {
            if (parents.Length != null)
            {
                
            }

            Energy = 50;
        }

        /// <summary>
        /// Performed on bot death
        /// </summary>
        /// <param name="killer"></param>
        public void OnDeath(IBot killer)
        {
        }

        public override string ToString() =>
            $"{GetType().Name}#{Id}";
    }
}