using ArtificialFarm.FarmMap;
using UnityEngine;

namespace ArtificialFarm.Core
{
    public interface IFarmObject
    {
        Population Pop { get; }
        WorldMap Map { get; }

        ICell Cell { get; set; }
        Turn Turn { get; }

        Color OnDisplay(DisplayMode mode);
    }
}