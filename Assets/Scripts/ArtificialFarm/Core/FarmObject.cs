using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm.Core
{
    /// <summary>
    /// Basic class for any object on the farm
    /// </summary>
    public abstract class FarmObject
    {
        private static uint _summaryIdIter = 100;

        public uint Id { get; }

        public Population Pop { get; }
        public WorldMap Map { get; }
        public Cell Cell { get; set; }
        public Turn Turn { get; }


        public abstract Color OnDisplay(DisplayMode mode);


        protected FarmObject()
        {
            Id = _summaryIdIter++;
            Pop = FarmSettings.Current.Pop;
            Map = FarmSettings.Current.Map;
            Turn = new Turn(-1);
        }
    }
}