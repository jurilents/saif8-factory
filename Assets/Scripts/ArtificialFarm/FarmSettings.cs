using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm
{
    public static class FarmSettings
    {
        public static uint SummaryPopulation;

        public static Farm Current { get; internal set; }


        public static DisplayMode DisplayMode { get; set; } = Defaults.DisplayMode;

        internal static sbyte TileNeighborsCount
        {
            set => Turn.TileNeighborsCount = value;
        }


        private static WaitForSeconds _waitingTime;

        public static WaitForSeconds StepWaitingTime
        {
            get => _waitingTime ?? (_waitingTime = Defaults.WaitSeconds);
            set => _waitingTime = value;
        }


        // private static Size? _mapSize;
        //
        // public static Size MapSize
        // {
        //     get
        //     {
        //         if (_mapSize == null) throw new ArgumentException();
        //         return (Size) _mapSize;
        //     }
        //     set => _mapSize = value;
        // }


        // private static Tilemap _tilemap;
        //
        // public static Tilemap Tilemap
        // {
        //     get
        //     {
        //         if (_tilemap == null) throw new ArgumentNullException();
        //         return _tilemap;
        //     }
        //     set => _tilemap = value;
        // }


        // private static Tile _tile;
        //
        // public static Tile TilePrefab
        // {
        //     get
        //     {
        //         if (_tile == null) throw new ArgumentNullException();
        //         return _tile;
        //     }
        //     set => _tile = value;
        // }
    }
}