﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ArtificialFarm.BotAI.DNAImplementations;
using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace ArtificialFarm.UI
{
	[RequireComponent(typeof(FarmUI))]
	public class Farm : MonoBehaviour
	{
		public Tilemap tilemap;
		public TileBase tilePrefab;

		public WorldMap Map { get; private set; }
		public Population Pop { get; private set; }
		public Dictionary<Type, GeneticCore> Dna { get; private set; }


		private FarmUI _ui;
		private bool _play;
		private ulong _step;


		/// <summary>
		/// Initialize new farm from FarmUI custom data
		/// </summary>
		public void Init()
		{
			Debug.Log("Starting farm initialization...");

			_ui = GetComponent<FarmUI>();

			FarmSettings.DisplayMode = _ui.displayMode;
			FarmSettings.Current = this;

			// switch (_settings.tileOrientation)
			// {
			// 	case GridOrientation.Square:
			// 		FarmSettings.TileNeighborsCount = 4;
			// 		Debug.LogError("GridOrientation.Square not implement");
			// 		break;
			//
			// 	case GridOrientation.HexPointTop:
			FarmSettings.TileNeighborsCount = 6;
			Map = new HexWorldMap(tilemap, tilePrefab, FarmSettings.size);
			// 		break;
			//
			// 	case GridOrientation.HexFlatTop:
			// 		FarmSettings.TileNeighborsCount = 6;
			// 		Debug.LogError("GridOrientation.HexFlatTop not implement");
			// 		break;
			//
			// 	default:
			// 		throw new ArgumentOutOfRangeException
			// 				(nameof(_ui.tileOrientation), _ui.tileOrientation, null);
			// }

			Dna = new Dictionary<Type, GeneticCore>();
			Pop = new Population();
			Pop.InitBotsPool((int) (FarmSettings.size.Summary * 1.05f));

			InjectGenome<TypeAlpha>();

			Debug.Log("Farm successfully initialized!");

			Pop.Spawn<TypeAlpha>((ushort) FarmSettings.initialPopulation);
		}


		/// <summary>
		/// Start life loop
		/// </summary>
		public void Play()
		{
			if (_play) return;
			Debug.Log("EVENT: Play");
			_play = true;
			StartCoroutine(LifeLoop());
		}

		/// <summary>
		/// Stop life loop
		/// </summary>
		public void Pause()
		{
			Debug.Log("EVENT: Pause");
			_play = !_play;
			if (_play)
				StartCoroutine(LifeLoop());
		}

		/// <summary>
		/// Stop life loop and reset last generation
		/// </summary>
		public void Stop()
		{
			Debug.Log("EVENT: Stop");
			_play = false;
			_step = 0;
		}


		/// <summary>
		/// Main loop to do iterations of life
		/// </summary>
		private IEnumerator LifeLoop()
		{
			Debug.Log("Farm life loop is running");

			var reserve = new List<float>();
			var population = new List<float>();


			while (_play)
			{
				var watch = new Stopwatch();
				watch.Start();

				_step++;

				Pop.ApplyChanges();

				foreach (var bot in Pop)
				{
					if (bot.IsAlive) bot.OnStep();
					else Pop.Kill(bot);
				}

				Map.Refresh();

				watch.Stop();

				// Debug.Log($"=============== {_step} # {watch.ElapsedMilliseconds}ms ===============" +
				//           $"\nPopulation: {Pop.Count}");

				_ui.UpdateStepField(_step);
				_ui.UpdatePopField(Pop.Count);

				reserve.Add(Pop.ReserveCount);
				population.Add(Pop.Count);

				if (Pop.Count == 0)
				{
					Debug.LogWarning("Zero population.");

					// GameObject obj = _ui.graphicContainer.gameObject;
					// obj.SetActive(true);

					// var graph = new GraphPanel(obj, 4500, 2);
					// graph.SetLineColor(Color.cyan, 0);
					// graph.AddPointsRange(reserve, 0);

					// graph.SetLineColor(Color.red, 1);
					// graph.AddPointsRange(population, 1);

					yield break;
				}

				yield return FarmSettings.StepWaitingTime;
			}
		}


		/// <summary>
		/// Append genotype to farm configuration
		/// </summary>
		/// <typeparam name="TBot">class implemented IDNA interface</typeparam>
		private void InjectGenome<TBot>() where TBot : IDNA
		{
			var mutation = new Mutation(FarmSettings.mutationChance, FarmSettings.mutationsCount);
			if (FarmSettings.mutationsCount > 0) mutation.Enabled = true;

			var geneticCore = new GeneticCore(typeof(TBot), FarmSettings.genomeLength, mutation);
			Dna.Add(typeof(TBot), geneticCore);
		}
	}
}