using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArtificialFarm.BotAI;

namespace ArtificialFarm.Core
{
    /// <summary>
    /// A collection of wrappers for all bots on the farm in any state of life
    /// </summary>
    public class Population : IEnumerable<IBot>
    {
        private readonly Dictionary<uint, IBot> _aliveBots;
        private readonly Queue<IBot> _reservePool;
        private readonly Queue<(IBot, IBot[])> _birthNote;
        private readonly Queue<IBot> _deathNote;

        /// <summary>
        /// Count of alive boys
        /// </summary>
        public int Count => _aliveBots.Count;


        /// <summary>
        /// First initialisation constructor with pre-building of all bots
        /// </summary>
        /// <param name="limit">Number of pre-build bots</param>
        public Population(int limit)
        {
            _aliveBots = new Dictionary<uint, IBot>();
            _reservePool = new Queue<IBot>();
            _birthNote = new Queue<(IBot, IBot[])>();
            _deathNote = new Queue<IBot>();

            for (int i = 0; i < limit; i++)
                _reservePool.Enqueue(new Bot());
        }

        public IEnumerator<IBot> GetEnumerator() => _aliveBots.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        /// <summary>
        /// Kill and give birth to all previously marked bots
        /// </summary>
        public void ApplyChanges()
        {
            foreach (var bot in _deathNote.Where(bot => _aliveBots.ContainsKey(bot.Id)))
            {
                bot.OnDeath(null);
                _reservePool.Enqueue(bot);
                // Debug.Log("BOT: " + bot.Cell);
                // Debug.Log("CELL: " + cell.Bot);
            }

            foreach (var (bot, parents) in _birthNote)
            {
                bot.OnBirth(parents);
                _aliveBots.Add(bot.Id, bot);
            }

            _birthNote.Clear();
        }


        /// <summary>
        /// Mark for further creation
        /// </summary>
        /// <param name="bot"></param>
        /// <param name="parents"></param>
        public void Birth(IBot bot, params IBot[] parents)
        {
            if (bot != null) _birthNote.Enqueue((bot, parents));
        }


        /// <summary>
        /// Mark for further destruction
        /// </summary>
        /// <param name="bot"></param>
        public void Kill(IBot bot)
        {
            if (bot != null) _deathNote.Enqueue(bot);
        }
    }
}