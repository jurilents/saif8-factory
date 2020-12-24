using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using ArtificialFarm.BotAI;
using ArtificialFarm.BotAI.Genetic;
using ArtificialFarm.BotIntelligences;
using ArtificialFarm.FarmMap;
using UnityEngine;
using UnityEngine.Tilemaps;
using Debug = UnityEngine.Debug;

namespace ArtificialFarm
{
    [RequireComponent(typeof(FarmUI))]
    public class Farm : MonoBehaviour
    {
        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private TileBase _tilePrefab;
        [SerializeField] private GridOrientation _tileOrientation;

        public WorldMap Map { get; private set; }
        public Population Pop { get; private set; }
        public Dictionary<Type, DNABase> Dna { get; private set; }

        private FarmUI _ui;
        private bool _play;
        private ulong _step;
        private Camera _camera;


        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var pos = _camera.ScreenToWorldPoint(Input.mousePosition);
                var cell = Map.GetCell((int)pos.x, (int)pos.y);
                if (cell != null)
                    Debug.Log($"Cell: x:{cell.Pos.x} y:{cell.Pos.y}\nbot:{cell.Bot != null}");
            }
        }


        /// <summary>
        /// Initialize new farm from FarmUI custom data
        /// </summary>
        public void Init()
        {
            _ui = GetComponent<FarmUI>();
            Pop = new Population();
            Dna = new Dictionary<Type, DNABase>();

            FarmSettings.DisplayMode = _ui.displayMode;
            FarmSettings.Current = this;

            switch (_tileOrientation)
            {
                case GridOrientation.Square:
                    FarmSettings.TileNeighborsCount = 4;
                    Debug.LogError("GridOrientation.Square not implement");
                    break;

                case GridOrientation.HexPointTop:
                    FarmSettings.TileNeighborsCount = 6;
                    Map = new HexWorldMap(_tilemap, _tilePrefab, _ui.Size);
                    break;

                case GridOrientation.HexFlatTop:
                    FarmSettings.TileNeighborsCount = 6;
                    Debug.LogError("GridOrientation.HexFlatTop not implement");
                    break;

                default:
                    throw new ArgumentOutOfRangeException
                        (nameof(_tileOrientation), _tileOrientation, null);
            }

            if (_ui.EnableTypeAlpha) InjectBot<TypeAlpha>();
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
        

        private void InjectBot<TBot>() where TBot : IBot, new()
        {
            var mutation = new Mutation(_ui.mutationChance, _ui.mutationsCount);
            if (_ui.mutationsCount > 0) mutation.Enabled = true;

            var dnaBase = new DNABase(typeof(TBot), _ui.dnaLength, mutation);
            Dna.Add(typeof(TBot), dnaBase);

            for (int i = 0; i < _ui.startPopulation; i++)
            {
                var bot = new TBot();
                Pop.Birth(bot);
            }
        }
    }
}