using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArtificialFarm.BotAI
{
    public class Population : IEnumerable<IBot>
    {
        private readonly Dictionary<uint, IBot> _bots;
        private readonly HashSet<IBot> _birthNote;
        private readonly HashSet<IBot> _deathNote;

        public Population()
        {
            _birthNote = new HashSet<IBot>();
            _deathNote = new HashSet<IBot>();
            _bots = new Dictionary<uint, IBot>();
        }


        public int Count => _bots.Count;

        public IEnumerator<IBot> GetEnumerator() => _bots.Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public void ApplyChanges()
        {
            foreach (var bot in _deathNote.Where(bot => _bots.ContainsKey(bot.Id)))
            {
                bot.OnDeath(null);
                var cell = bot.Cell;
                bot.Cell = null;
                _bots.Remove(bot.Id);
                Debug.Log("BOT: " + bot.Cell);
                Debug.Log("CELL: " + cell.Bot);
            }

            foreach (var bot in _birthNote.Where(bot => !_bots.ContainsKey(bot.Id)))
            {
                _bots.Add(bot.Id, bot);
            }

            _birthNote.Clear();
        }

        public void Birth(IBot bot)
        {
            if (bot != null) _birthNote.Add(bot);
        }

        public void Kill(IBot bot)
        {
            if (bot != null) _deathNote.Add(bot);
        }
    }
}