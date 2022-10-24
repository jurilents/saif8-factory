using System;
using System.Collections.Generic;
using System.Linq;
using ArtificialFarm.Core;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace ArtificialFarm.BotAI.Genetic
{
	public class GeneticCore
	{
		private Mutation _mutation;

		private readonly Gene[] _allGenes;
		private readonly Dictionary<ushort, Gene> _availableGenes;

		private readonly float _dietCount;

		public int AvailableGenesCount => _availableGenes.Count;
		public uint DNALength { get; }


		public Gene this[byte id] => _availableGenes.ContainsKey(id)
				? _availableGenes[id]
				: _availableGenes.Values.FirstOrDefault(g => g.Available);


		public GeneticCore(Type botType, uint length, Mutation mutation)
		{
			DNALength = length <= 0 ? Defaults.GenomeLength : length;

			// Get only genes from own bot class
			var methodInfoList = Reflection.GetMethodsWithAttribute<GeneAttribute>(botType).ToList();
			int methodsCount = methodInfoList.Count;

			string className = Reflection.GetTypeName(botType);
			Debug.Log($"Applying genes [{className}] => {methodsCount}");
			if (methodsCount == 0) throw new ArrayTypeMismatchException();

			_allGenes = new Gene[methodsCount];

			// detect all methods which will be used as genes implementations
			for (int i = 0; i < methodsCount; i++)
			{
				var methodInfo = methodInfoList[i];
				var attr = (GeneAttribute) methodInfo.GetCustomAttributes(typeof(GeneAttribute), true)[0];
				var foo = (GeneAction) Delegate.CreateDelegate(typeof(GeneAction), methodInfo);
				_allGenes[i] = new Gene(foo, attr.Group, attr.Name);
			}

			_mutation = mutation;
			_availableGenes = _allGenes.Where(g => g.Available).ToDictionary(g => g.Id);

			_dietCount = Enum.GetValues(typeof(Diet)).Length - 1f;
		}


		public void SetActive(byte geneId, bool state)
		{
			if (state)
			{
				if (_availableGenes.ContainsKey(geneId)) return;
				if (_allGenes.Any(g => g.Id == geneId))
					_availableGenes.Remove(geneId);
				else throw new ArgumentException();
			}
			else
			{
				if (!_availableGenes.ContainsKey(geneId)) return;
				_availableGenes.Remove(geneId);
			}

			this[geneId].SetActive(state);
		}

		public byte[] GenerateGenome(bool constant = false)
		{
			var genome = new byte[DNALength];
			if (constant)
			{
				byte defaultGeneId = RandomGeneId();
				for (int i = 0; i < genome.Length; i++)
					genome[i] = defaultGeneId;
			}
			else
			{
				for (int i = 0; i < genome.Length; i++)
					genome[i] = RandomGeneId();
			}

			return genome;
		}

		public void Mutate(byte[] genes)
		{
			if (!_mutation.Enabled) return;

			for (int i = 0; i < _mutation.Count; i++)
			{
				if (RandMe.TryLuck(_mutation.Chance))
					genes[RandMe.RandIndex(genes)] = RandomGeneId();
			}
		}

		public float MutateDietValue(float dietValue, float addValue, float subValue)
		{
			if (!_mutation.Enabled) return dietValue;
			if (RandMe.TryLuck(_mutation.Chance)) dietValue += addValue;
			if (RandMe.TryLuck(_mutation.Chance)) dietValue -= subValue;
			return Mathf.Clamp(dietValue, 0, _dietCount);
		}

		public byte RandomGeneId() => (byte) Random.Range(0, AvailableGenesCount);
	}
}