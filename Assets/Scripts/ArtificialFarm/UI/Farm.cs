using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.Core;
using ArtificialFarm.FarmMap;
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
            _ui = GetComponent<FarmUI>();
            Pop = new Population(_ui.Size.Summary);
            Dna = new Dictionary<Type, GeneticCore>();

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
        }


        /// <summary>
        /// 
        /// </summary>
        public void Play()
        {
            _play = true;
            StartCoroutine(LifeLoop());
        }

        /// <summary>
        /// 
        /// </summary>
        public void Pause()
        {
            _play = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Stop()
        {
            _play = false;
            _step = 0;
        }


        /// <summary>
        /// Main game cycle
        /// </summary>
        private IEnumerator LifeLoop()
        {
            while (_play)
            {
                var watch = new Stopwatch();
                watch.Start();

                _step++;
                _ui.UpdateStepField(_step);
                _ui.UpdatePopField(Pop.Count);

                foreach (var bot in Pop)
                {
                    bot.OnStep();
                    if (!bot.IsAlive) Pop.Kill(bot);
                }

                Pop.ApplyChanges();
                Map.Refresh();

                watch.Stop();

                // Debug.Log($"=============== {_step} # {watch.ElapsedMilliseconds}ms ===============" +
                // $"\nPopulation: {Pop.Count}");

                yield return FarmSettings.StepWaitingTime;
            }
        }


        // private void InjectBot<TBot>() where TBot : IBot, new()
        // {
        //     var mutation = new Mutation(_ui.mutationChance, _ui.mutationsCount);
        //     if (_ui.mutationsCount > 0) mutation.Enabled = true;
        //
        //     var dnaBase = new DNABase(typeof(TBot), _ui.dnaLength, mutation);
        //     Dna.Add(typeof(TBot), dnaBase);
        //
        //     for (int i = 0; i < _ui.startPopulation; i++)
        //     {
        //         var bot = new TBot();
        //         Pop.Birth(bot);
        //     }
        // }
    }
}