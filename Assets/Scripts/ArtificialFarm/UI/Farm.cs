using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ArtificialFarm.BotAI.DNAImplementations;
using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
using Core.GraphTools;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ArtificialFarm.UI
{
    [RequireComponent(typeof(FarmUI))]
    public class Farm : MonoBehaviour
    {
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

            switch (_ui.TileOrientation)
            {
                case GridOrientation.Square:
                    FarmSettings.TileNeighborsCount = 4;
                    Debug.LogError("GridOrientation.Square not implement");
                    break;

                case GridOrientation.HexPointTop:
                    FarmSettings.TileNeighborsCount = 6;
                    Map = new HexWorldMap(_ui.Tilemap, _ui.TilePrefab, _ui.Size);
                    break;

                case GridOrientation.HexFlatTop:
                    FarmSettings.TileNeighborsCount = 6;
                    Debug.LogError("GridOrientation.HexFlatTop not implement");
                    break;

                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(_ui.TileOrientation), _ui.TileOrientation, null);
            }

            Dna = new Dictionary<Type, GeneticCore>();
            Pop = new Population();
            Pop.InitBotsPool((int) (_ui.Size.Summary * 1.05f));

            InjectGenome<TypeAlpha>();

            Debug.Log("Farm successfully initialized!");

            Pop.Spawn<TypeAlpha>(3);
        }


        /// <summary>
        /// Start life loop
        /// </summary>
        public void Play()
        {
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
            _play = false;
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

                    var obj = _ui.graphicContainer.gameObject;
                    obj.SetActive(true);

                    var graph = new GraphPanel(obj, 4500, 2);
                    graph.SetLineColor(Color.cyan, 0);
                    graph.AddPointsRange(reserve, 0);

                    graph.SetLineColor(Color.red, 1);
                    graph.AddPointsRange(population, 1);

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
            var mutation = new Mutation(_ui.mutationChance, _ui.mutationsCount);
            if (_ui.mutationsCount > 0) mutation.Enabled = true;

            var geneticCore = new GeneticCore(typeof(TBot), _ui.dnaLength, mutation);
            Dna.Add(typeof(TBot), geneticCore);
        }
    }
}