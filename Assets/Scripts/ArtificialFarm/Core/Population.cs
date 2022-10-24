using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArtificialFarm.BotAI;
using ArtificialFarm.BotAI.Genetic;
using JetBrains.Annotations;
using UnityEngine;

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

		// TODO: remove it
		public int ReserveCount => _reservePool.Count;


		/// <summary>
		/// First initialisation constructor with pre-building of all bots
		/// </summary>
		public Population()
		{
			_aliveBots = new Dictionary<uint, IBot>();
			_reservePool = new Queue<IBot>();
			_birthNote = new Queue<(IBot, IBot[])>();
			_deathNote = new Queue<IBot>();
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
				_aliveBots.Remove(bot.Id);
				_reservePool.Enqueue(bot);
			}

			_deathNote.Clear();

			foreach (var (bot, parents) in _birthNote)
			{
				if (bot.OnBirth(parents))
					_aliveBots.Add(bot.Id, bot);
				else _reservePool.Enqueue(bot);
			}

			_birthNote.Clear();

			// Debug.Log("POOL: " + _reservePool.Count);
		}


		public void InitBotsPool(int limit)
		{
			if (limit < 1) throw new ArgumentOutOfRangeException();

			for (int i = 0; i < limit; i++)
				_reservePool.Enqueue(new Bot());
		}


		public void Spawn<TGenome>(ushort count = 1) where TGenome : IDNA
		{
			FarmSettings.DefaultGenes = typeof(TGenome);

			for (int i = 0; i < count; i++)
			{
				var instance = _reservePool.Dequeue();
				// var instance = new Bot();
				_birthNote.Enqueue((instance, null));
			}

			ApplyChanges();
		}


		/// <summary>
		/// Mark for further creation
		/// </summary>
		/// <param name="parents">Parent bot(s) (at least one)</param>
		public void BirthChildFor([NotNull] params IBot[] parents)
		{
			if (parents.Length <= 0)
				throw new ArgumentException("The child must have at least one parent");

			if (_reservePool.Count > 0)
			{
				var instance = _reservePool.Dequeue();
				// var instance = new Bot();
				_birthNote.Enqueue((instance, parents));
			}
			else Debug.LogWarning("Population reserve pool is empty!");
		}


		/// <summary>
		/// Mark for further destruction
		/// </summary>
		/// <param name="bot"></param>
		public void Kill([NotNull] IBot bot)
		{
			_deathNote.Enqueue(bot);
		}
	}
}